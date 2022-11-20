import { Component, OnInit } from '@angular/core';
import { StatisticsService } from '@app/shared/services';
import { BaseComponent } from '@app/protected-zone/base/base.component';
import {  MonthlyNewMember } from '@app/shared/models';
import * as FileSaver from 'file-saver';
import jsPDF from "jspdf";
import "jspdf-autotable";

@Component({
  selector: 'app-monthly-new-members',
  templateUrl: './monthly-new-members.component.html',
  styleUrls: ['./monthly-new-members.component.css']
})
export class MonthlyNewMembersComponent extends BaseComponent implements OnInit {

  public screenTitle: string;
  // Default
  public blockedPanel = false;
  // Customer Receivable
  public items: any[];
  public year: number = new Date().getFullYear();
  public totalItems = 0;

  public monthlyNewMember : MonthlyNewMember[]
  cols: any[];
  exportColumns: any[];

  constructor(
    private statisticService: StatisticsService) {
    super('STATISTIC_MONTHLY_NEWMEMBER');

  }
  ngOnInit() {
    super.ngOnInit();
    this.loadData();
    this.statisticService.getMonthlyNewMembers(this.year).subscribe((response: any) => {   
      this.monthlyNewMember = response;     
    });

    this.cols = [
      { field: 'month', header: 'Month' },
      { field: 'numberOfRegisters', header: 'Number of register' }
  ];

  this.exportColumns = this.cols.map(col => ({title: col.header, dataKey: col.field}));

  }

  loadData() {
    this.blockedPanel = true;
    this.statisticService.getMonthlyNewMembers(this.year)
      .subscribe((response: any) => {
        this.totalItems = 0;
        this.items = response;
        response.forEach(element => {
          this.totalItems += element.NumberOfRegisters;
        });
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      });
  }

  exportPdf() {     
     const doc = new jsPDF('p','pt');
     doc.text("Number of new member in " + this.year, 100, 30);
     doc['autoTable'](this.exportColumns, this.monthlyNewMember);
     doc.save("monthlyNewMember" + new Date().getTime()+".pdf");
}

exportExcel() {
    import("xlsx").then(xlsx => {
        const worksheet = xlsx.utils.json_to_sheet(this.monthlyNewMember);
        const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
        const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
        this.saveAsExcelFile(excelBuffer, "monthlyNewMember");
    });
}

saveAsExcelFile(buffer: any, fileName: string): void {
    let EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
    let EXCEL_EXTENSION = '.xlsx';
    const data: Blob = new Blob([buffer], {
        type: EXCEL_TYPE
    });
    FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
}
}
