<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128631713/11.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1933)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# WinForms Data Grid - Display error icons within unbound column cells

This example shows how to handle the [CustomDrawCell](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.Grid.GridView.CustomDrawCell) event to indicate invalid values (display error icons within data cells based on a specific condition).

![](https://raw.githubusercontent.com/DevExpress-Examples/how-to-show-error-icons-for-cells-which-belong-to-an-unbound-column-gridcontrol-e1933/11.1.4%2B/media/winforms-grid-show-error-icons-in-unbound-column.png)

```csharp
private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
    BaseEditViewInfo info = ((GridCellInfo)e.Cell).ViewInfo;
    string error = GetError(e.CellValue, e.RowHandle, e.Column);
    SetError(info, error);
    info.CalcViewInfo(e.Cache.Graphics);
}
public string GetError(object value, int rowHandle, GridColumn column) {
    if (value == null) return "value is null";
    if (!(value is int)) return string.Empty;
    if ((int)value % 2 == 0) return "value is even";
    if ((int)value < 10) return "value < 10";
    return string.Empty;
}
void SetError(BaseEditViewInfo cellInfo, string errorIconText) {
    if (errorIconText == string.Empty) return;
    cellInfo.ErrorIconText = errorIconText;
    cellInfo.ShowErrorIcon = true;
    cellInfo.FillBackground = true;
    cellInfo.ErrorIcon = DXErrorProvider.GetErrorIconInternal(ErrorType.Critical);
}
```


## Files to Review

* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))


## Documentation

* [Indicate Invalid Values with IDXDataErrorInfo](https://docs.devexpress.com/WindowsForms/753/controls-and-libraries/data-grid/data-editing-and-validation/modify-and-validate-cell-values#indicate-invalid-values-with-idxdataerrorinfo)
* [Custom Painting Basics](https://docs.devexpress.com/WindowsForms/762/controls-and-libraries/data-grid/appearance-and-conditional-formatting/custom-painting/custom-painting-basics)


## See Also

* [How to show error icons for cells when working in unbound mode](https://supportcenter.devexpress.com/ticket/details/k18308/how-to-show-error-icons-for-cells-when-working-in-unbound-mode)
* [Access, Modify and Validate Rows and Cells of Data-Aware Control - Cheat Sheets and Best Practices](https://supportcenter.devexpress.com/ticket/details/t904183/access-modify-and-validate-rows-and-cells-of-a-data-aware-control-winforms-cheat-sheet)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-grid-show-error-icons&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-grid-show-error-icons&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
