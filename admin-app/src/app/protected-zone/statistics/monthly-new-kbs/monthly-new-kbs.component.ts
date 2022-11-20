import { Component, OnInit } from '@angular/core';
import { StatisticsService } from '@app/shared/services';
import { BaseComponent } from '@app/protected-zone/base/base.component';
import {  MonthlyNewKb } from '@app/shared/models';
import * as FileSaver from 'file-saver';
import jsPDF from "jspdf";
import "jspdf-autotable";

@Component({
  selector: 'app-monthly-new-kbs',
  templateUrl: './monthly-new-kbs.component.html',
  styleUrls: ['./monthly-new-kbs.component.css']
})
export class MonthlyNewKbsComponent extends BaseComponent implements OnInit {
// Default
public blockedPanel = false;
public screenTitle: string;

// Customer Receivable
public items: any[];
public year: number = new Date().getFullYear();
public totalItems = 0;
public monthlyNewKb : MonthlyNewKb[]
  cols: any[];
  exportColumns: any[];

constructor(
  private statisticService: StatisticsService) {
    super('STATISTIC_MONTHLY_NEWKB');

}
ngOnInit() {
  super.ngOnInit();
  this.loadData();
 this.statisticService.getMonthlyNewKbs(this.year).subscribe((response: any) => {   
      this.monthlyNewKb = response;     
    });

    this.cols = [
      { field: 'month', header: 'Tháng' },
      { field: 'numberOfNewKbs', header: 'Number of new KnowledgeSpace' }
  ];

  this.exportColumns = this.cols.map(col => ({title: col.header, dataKey: col.field}));
}
loadData() {
  this.blockedPanel = true;
  this.statisticService.getMonthlyNewKbs(this.year)
    .subscribe((response: any) => {
      this.totalItems = 0;
      this.items = response;
      response.forEach(element => {
        this.totalItems += element.NumberOfNewKbs;
      });
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    });
}
exportPdf() {     
  const doc = new jsPDF('p','pt');
  doc.text("Number of KnowledgeSpace in " + this.year, 100, 30);
  doc['autoTable'](this.exportColumns, this.monthlyNewKb);
  doc.save("monthlyNewKb" + new Date().getTime()+".pdf");
}

exportExcel() {
 import("xlsx").then(xlsx => {
     const worksheet = xlsx.utils.json_to_sheet(this.monthlyNewKb);
     const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
     const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
     this.saveAsExcelFile(excelBuffer, "NumberOfNewKbs");
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
