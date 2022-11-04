import { Component, OnInit, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NotificationService, UtilitiesService, CommandsService, RolesService } from '@app/shared/services';
import { SystemConstants, MessageConstants } from '@app/shared/constants';
import { TreeNode } from 'primeng/api/treenode';
import { PermissionsService } from '@app/shared/services/permissions.service';
import { PermissionUpdateRequest } from '@app/shared/models';
import { Permission } from '@app/shared/models/permission.model';
import { Subscription } from 'rxjs';
import { BaseComponent } from '@app/protected-zone/base/base.component';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.css']
})
export class PermissionsComponent extends BaseComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  public screenTitle: string;
  public bsModalRef: BsModalRef;
  public blockedPanel = false;

  public functions: any[];
  public flattenFunctions: any[] = [];
  public selectedRole: any = {
    id: null
  };
  public roles: any[] = [];
  public commands: any[] = [];

  public selectedViews: string[] = [];
  public selectedCreates: string[] = [];
  public selectedUpdates: string[] = [];
  public selectedDeletes: string[] = [];
  public selectedApproves: string[] = [];

  public isSelectedAllViews = false;
  public isSelectedAllCreates = false;
  public isSelectedAllUpdates = false;
  public isSelectedAllDeletes = false;
  public isSelectedAllApproves = false;

  constructor(

    private permissionsService: PermissionsService,
    private rolesService: RolesService,
    private commandsService: CommandsService,
    private _notificationService: NotificationService,
    private _utilityService: UtilitiesService) {
      super('SYSTEM_PERMISSION');
  }


  ngOnInit() {
    super.ngOnInit();
    this.loadAllRoles();
    this.loadData(this.selectedRole.id);
  }

  changeRole($event: any) {
    if ($event.value != null) {
      this.loadData($event.value.id);
    } else {
      this.functions = [];
    }
  }
  public reloadData() {
    this.loadData(this.selectedRole.id.id);
  }
  public savePermission() {
    if (this.selectedRole.id == null) {
      this._notificationService.showError('Bạn chưa chọn nhóm quyền.');
      return;
    }
    const listPermissions: Permission[] = [];
    this.selectedCreates.forEach(element => {
      listPermissions.push({
        functionId: element,
        roleId: this.selectedRole.id.id,
        commandId: SystemConstants.CREATE_ACTION
      });
    });
    this.selectedUpdates.forEach(element => {
      listPermissions.push({
        functionId: element,
        roleId: this.selectedRole.id.id,
        commandId: SystemConstants.UPDATE_ACTION
      });
    });
    this.selectedDeletes.forEach(element => {
      listPermissions.push({
        functionId: element,
        roleId: this.selectedRole.id.id,
        commandId: SystemConstants.DELETE_ACTION
      });
    });
    this.selectedViews.forEach(element => {
      listPermissions.push({
        functionId: element,
        roleId: this.selectedRole.id.id,
        commandId: SystemConstants.VIEW_ACTION
      });
    });

    this.selectedApproves.forEach(element => {
      listPermissions.push({
        functionId: element,
        roleId: this.selectedRole.id.id,
        commandId: SystemConstants.APPROVE_ACTION
      });
    });
    const permissionsUpdateRequest = new PermissionUpdateRequest();
    permissionsUpdateRequest.permissions = listPermissions;
    this.subscription.add(this.permissionsService.save(this.selectedRole.id.id, permissionsUpdateRequest)
      .subscribe(() => {
        this._notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);

        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }));
  }
  loadData(roleId) {
    if (roleId != null) {
      this.blockedPanel = true;
      this.subscription.add(this.permissionsService.getFunctionWithCommands()
        .subscribe((response: any) => {
          const unflattering = this._utilityService.UnflatteringForTree(response);
          this.functions = <TreeNode[]>unflattering;
          this.flattenFunctions = response;
          this.fillPermissions(roleId);
          setTimeout(() => { this.blockedPanel = false; }, 1000);
        }, error => {
          setTimeout(() => { this.blockedPanel = false; }, 1000);
        }));
    }

  }


  fillPermissions(roleId: any) {
    this.blockedPanel = true;
    this.subscription.add(this.rolesService.getRolePermissions(roleId)
      .subscribe((response: Permission[]) => {
        this.selectedCreates = [];
        this.selectedUpdates = [];
        this.selectedDeletes = [];
        this.selectedViews = [];
        this.selectedApproves = [];
        response.forEach(element => {
          if (element.commandId === SystemConstants.CREATE_ACTION) {
            this.selectedCreates.push(element.functionId);
          }
          if (element.commandId === SystemConstants.UPDATE_ACTION) {
            this.selectedUpdates.push(element.functionId);
          }
          if (element.commandId === SystemConstants.DELETE_ACTION) {
            this.selectedDeletes.push(element.functionId);
          }
          if (element.commandId === SystemConstants.VIEW_ACTION) {
            this.selectedViews.push(element.functionId);
          }
          if (element.commandId === SystemConstants.APPROVE_ACTION) {
            this.selectedApproves.push(element.functionId);
          }
          setTimeout(() => { this.blockedPanel = false; }, 1000);
        });

      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }));
  }
  loadAllRoles() {
    this.blockedPanel = true;
    this.subscription.add(this.rolesService.getAll()
      .subscribe((response: any) => {
        this.roles = response;
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
