﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v15.2.Core, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script lang="cs">
        function OnClientPopupMenuShowing(s, e) {
            // 1st approach
            if (MainScheduler.GetSelectedAppointmentIds().length > 0) {
                var selectedAppointment = MainScheduler.GetAppointmentById(MainScheduler.GetSelectedAppointmentIds()[0]);
                for (menuItemId in e.item.items) {
                    if (e.item.items[menuItemId].name == "CustomValues") {
                        for (subMenuItemId in e.item.items[menuItemId].items) {
                            if (e.item.items[menuItemId].items[subMenuItemId].GetText() == selectedAppointment.CustomFieldValue) {
                                e.item.items[menuItemId].items[subMenuItemId].SetChecked(true);
                            }
                        }                        
                    }
                }
            }            
        }

        function OnClientItemClick(s, e) {
            if (e.item.name.indexOf("Custom Item") != -1) {
                MainScheduler.PerformCallback(e.item.GetText());
            }
            else {
                aspxSchedulerOnAptMenuClick(s, e);
            }
        }
    </script>

    <form id="form1" runat="server">
        <div>
            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" AppointmentDataSourceID="ObjectDataSourceAppointment" 
                ClientIDMode="AutoID" Start="2013-10-30" GroupType="Date" ClientInstanceName="MainScheduler" OnCustomCallback="ASPxScheduler1_CustomCallback" OnInitAppointmentDisplayText="ASPxScheduler1_InitAppointmentDisplayText"
                ResourceDataSourceID="ObjectDataSourceResources" OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing" OnInitClientAppointment="ASPxScheduler1_InitClientAppointment">
                <Storage>
                    <Appointments AutoRetrieveId="True">
                        <Mappings 
                            AllDay="AllDay" 
                            AppointmentId="Id" 
                            Description="Description" 
                            End="EndTime" 
                            Label="Label" 
                            Location="Location" 
                            ReminderInfo="ReminderInfo" 
                            ResourceId="OwnerId" 
                            Start="StartTime" 
                            Status="Status" 
                            Subject="Subject" 
                            Type="EventType" />
                        <CustomFieldMappings>
                            <dxwschs:ASPxAppointmentCustomFieldMapping Member="CustomField" Name="ApptCustomField" ValueType="String" />
                        </CustomFieldMappings>
                    </Appointments>
                    <Resources>
                        <Mappings 
                            Caption="Name" 
                            ResourceId="ResID" />
                    </Resources>
                </Storage>

                <Views>
                    <DayView>
                        <TimeRulers>
                            <cc1:TimeRuler AlwaysShowTimeDesignator="False" TimeMarkerVisibility="Never"></cc1:TimeRuler>
                        </TimeRulers>
                        <DayViewStyles ScrollAreaHeight="600px">
                        </DayViewStyles>
                    </DayView>

                    <WorkWeekView>
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                    </WorkWeekView>
                </Views>
                <OptionsCustomization AllowAppointmentEdit="Custom" />
            </dxwschs:ASPxScheduler>
            <br />
            <br />
            <asp:Button ID="ButtonPostBack" runat="server" Text="Post Back" />

            <asp:ObjectDataSource ID="ObjectDataSourceResources" runat="server" OnObjectCreated="ObjectDataSourceResources_ObjectCreated" SelectMethod="SelectMethodHandler" TypeName="WebApplication1.CustomResourceDataSource"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceAppointment" runat="server" DataObjectTypeName="WebApplication1.CustomAppointment" DeleteMethod="DeleteMethodHandler" InsertMethod="InsertMethodHandler" SelectMethod="SelectMethodHandler" TypeName="WebApplication1.CustomAppointmentDataSource" UpdateMethod="UpdateMethodHandler" OnObjectCreated="ObjectDataSourceAppointment_ObjectCreated"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
