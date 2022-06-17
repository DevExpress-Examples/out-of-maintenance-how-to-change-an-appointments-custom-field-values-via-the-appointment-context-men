<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.1, Version=18.1.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v18.1.Core, Version=18.1.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script lang="cs">
        function OnClientPopupMenuShowing(s, e) {
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

        function onMenuItemClicked(s, e) {
            if (e.itemName.indexOf("Custom Item") != -1) {
                MainScheduler.PerformCallback(e.itemName);
            }
        }
    </script>

    <form id="form1" runat="server">
        <div>
            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" AppointmentDataSourceID="ObjectDataSourceAppointment" 
                ClientIDMode="AutoID" GroupType="Date" ClientInstanceName="MainScheduler" OnCustomCallback="ASPxScheduler1_CustomCallback" OnInitAppointmentDisplayText="ASPxScheduler1_InitAppointmentDisplayText"
                ResourceDataSourceID="ObjectDataSourceResources" OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing" OnInitClientAppointment="ASPxScheduler1_InitClientAppointment">
                <ClientSideEvents MenuItemClicked="onMenuItemClicked" />
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
