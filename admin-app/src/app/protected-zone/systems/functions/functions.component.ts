import { Component, OnInit , OnDestroy } from '@angular/core';
import { TreeNode } from 'primeng/api';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FunctionsService } from '@app/shared/services/functions.service';
import { NotificationService, UtilitiesService } from '@app/shared/services';
import { FunctionsDetailComponent } from './functions-detail/functions-detail.component';
import { MessageConstants } from '@app/shared/constants';
import { CommandAssign } from '@app/shared/models';
import { CommandsAssignComponent } from './commands-assign/commands-assign.component';
import { Subscription } from 'rxjs';
import { BaseComponent } from '@app/protected-zone/base/base.component';

@Component({
  selector: 'app-functions',
  templateUrl: './functions.component.html',
  styleUrls: ['./functions.component.css']
})
export class FunctionsComponent extends BaseComponent implements OnInit , OnDestroy {
  private subscription = new Subscription();
  public bsModalRef: BsModalRef;
  public blockedPanel = false;
  public blockedPanelCommand = false;
  public showCommandGrid = false;
  // -----------------Function-----------------
  public items: TreeNode[] = [];
  public selectedItems = [];

  // ---------------Command------------------------------
  public commands: any[] = [];
  public selectedCommandItems = [];
  public screenTitle: string;
  constructor(
    private modalService: BsModalService,
    private functionsService: FunctionsService,
    private notificationService: NotificationService,
    private utilitiesService: UtilitiesService) {
      super('SYSTEM_FUNCTION');
  }

  ngOnInit() {
    super.ngOnInit();
    this.loadData();
  }

  togglePanel() {
    if (this.showCommandGrid) {
      if (this.selectedItems.length === 1) {
        this.loadDataCommand();
      }
    }

  }
  loadData(selectionId = null) {
    this.blockedPanel = true;
    this.subscription.add(this.functionsService.getAll()
      .subscribe((response: any) => {
        const functionTree = this.utilitiesService.UnflatteringForTree(response);
        this.items = <TreeNode[]>functionTree;
        if (this.selectedItems.length === 0 && this.items.length > 0) {
          this.selectedItems.push(this.items[0]);
          this.loadDataCommand();
        }
        // N???u c?? l?? s???a th?? ch???n selection theo Id
        if (selectionId != null && this.items.length > 0) {
          this.selectedItems = this.items.filter(x => x.data.id == selectionId);
        }

        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }));
  }

  showAddModal() {
    this.bsModalRef = this.modalService.show(FunctionsDetailComponent,
      {
        class: 'modal-lg',
        backdrop: 'static'
      });

    this.bsModalRef.content.saved.subscribe(response => {
      this.bsModalRef.hide();
      this.loadData();
      this.selectedItems = [];
    });
  }

  showEditModal() {
    if (this.selectedItems.length === 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const initialState = {
      entityId: this.selectedItems[0].data.id
    };
    this.bsModalRef = this.modalService.show(FunctionsDetailComponent,
      {
        initialState: initialState,
        class: 'modal-lg',
        backdrop: 'static'
      });


    this.bsModalRef.content.saved.subscribe((response) => {
      this.bsModalRef.hide();
      this.loadData(response.id);
    });
  }

  nodeSelect(event) {
    this.selectedCommandItems = [];
    this.commands = [];    
   
    if (this.selectedItems.length === 1 && this.showCommandGrid) {
      this.loadDataCommand();
     // this.selectedItems = [];
    }
  }

  nodeUnselect(event) {
    this.selectedCommandItems = [];
    this.commands = [];
    if (this.selectedItems.length === 1 && this.showCommandGrid) {
      this.loadDataCommand();
    }
  }

  deleteItems() {
    if (this.selectedItems.length === 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const id = this.selectedItems[0].id;
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteItemsConfirm(id));
  }

  deleteItemsConfirm(id: string) {
    this.blockedPanel = true;
    this.functionsService.delete(id).subscribe(() => {
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.loadData();
      this.selectedItems = [];
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    });
  }
  loadDataCommand() {
    this.blockedPanelCommand = true;
    this.subscription.add(this.functionsService.getAllCommandsByFunctionId(this.selectedItems[0].id)
      .subscribe((response: any) => {

        this.commands = response;
        if (this.selectedCommandItems.length === 0 && this.commands.length > 0) {
          this.selectedCommandItems.push(this.commands[0]);
        }
        this.blockedPanelCommand = false;
      }, error => {
        this.blockedPanelCommand = false;
      }));
  }

  removeCommands() {
    const selectedCommandIds = [];
    this.selectedCommandItems.forEach(element => {
      selectedCommandIds.push(element.id);
    });
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.removeCommandsConfirm(selectedCommandIds));
  }

  removeCommandsConfirm(ids: string[]) {
    this.blockedPanelCommand = true;
    const entity = new CommandAssign();
    entity.commandIds = ids;
    this.functionsService.deleteCommandsFromFunction(this.selectedItems[0].id, entity).subscribe(() => {
      this.loadDataCommand();
      this.selectedCommandItems = [];
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.blockedPanelCommand = false;
    }, error => {
      this.blockedPanelCommand = false;
    });
  }

  addCommandsToFunction() {
    if (this.selectedItems.length === 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const initialState = {
      existingCommands: this.commands.map(x => x.id),
      functionId: this.selectedItems[0].id
    };
    this.bsModalRef = this.modalService.show(CommandsAssignComponent,
      {
        initialState: initialState,
        class: 'modal-lg',
        backdrop: 'static'
      });
    this.bsModalRef.content.chosenEvent.subscribe((response: any[]) => {
      this.bsModalRef.hide();
      this.loadDataCommand();
      this.selectedCommandItems = [];
    });
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}