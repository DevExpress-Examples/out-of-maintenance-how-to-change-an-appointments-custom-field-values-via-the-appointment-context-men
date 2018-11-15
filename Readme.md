<!-- default file list -->
*Files to look at*:

* [CustomDataSource.cs](./CS/WebApplication1/CustomDataSource.cs) (VB: [CustomDataSource.vb](./VB/WebApplication1/CustomDataSource.vb))
* [CustomObjects.cs](./CS/WebApplication1/CustomObjects.cs) (VB: [CustomObjects.vb](./VB/WebApplication1/CustomObjects.vb))
* [Default.aspx](./CS/WebApplication1/Default.aspx) (VB: [Default.aspx](./VB/WebApplication1/Default.aspx))
* [Default.aspx.cs](./CS/WebApplication1/Default.aspx.cs) (VB: [Default.aspx](./VB/WebApplication1/Default.aspx))
<!-- default file list end -->
# How to change an appointment's custom field values via the "Appointment" context menu (like for labels and statuses)


<p>This example demonstrates how you can add possible values of an appointmen's custom field as additional items into the appointment's context menu, so that an end-user can change the appointmen's custom filed value in a similar manner as for labels and statuses.<br />The checked state of these additional items is synchronized with an appointment's custom field value in the client-side ASPxMenu.Popup event handler.<br />The appointment's custom field value is changed on the ASPxScheduler's custom callback after an end-user clicks a corresponding menu item. </p>

<br/>


