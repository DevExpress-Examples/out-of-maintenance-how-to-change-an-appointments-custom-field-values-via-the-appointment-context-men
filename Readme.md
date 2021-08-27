<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128545949/13.2.10%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T164645)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomDataSource.cs](./CS/WebApplication1/CustomDataSource.cs) (VB: [CustomDataSource.vb](./VB/WebApplication1/CustomDataSource.vb))
* [CustomObjects.cs](./CS/WebApplication1/CustomObjects.cs) (VB: [CustomObjects.vb](./VB/WebApplication1/CustomObjects.vb))
* [Default.aspx](./CS/WebApplication1/Default.aspx) (VB: [Default.aspx](./VB/WebApplication1/Default.aspx))
* [Default.aspx.cs](./CS/WebApplication1/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebApplication1/Default.aspx.vb))
<!-- default file list end -->
# How to change an appointment's custom field values via the "Appointment" context menu (like for labels and statuses)
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t164645/)**
<!-- run online end -->


<p>This example demonstrates how you can add possible values of an appointmen's custom field as additional items into the appointment's context menu, so that an end-user can change the appointmen's custom filed value in a similar manner as for labels and statuses.<br />The checked state of these additional items is synchronized with an appointment's custom field value in the client-side ASPxMenu.Popup event handler.<br />The appointment's custom field value is changed on the ASPxScheduler's custom callback after an end-user clicks a corresponding menu item. </p>

<br/>


