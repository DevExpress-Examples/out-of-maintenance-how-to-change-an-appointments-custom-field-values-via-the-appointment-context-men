using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1 {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        List<string> customFieldValues = new List<string>();
        protected void ObjectDataSourceResources_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
            if(Session["CustomResourceDataSource"] == null) {
                Session["CustomResourceDataSource"] = new CustomResourceDataSource(GetCustomResources());
            }
            e.ObjectInstance = Session["CustomResourceDataSource"];
        }

        BindingList<CustomResource> GetCustomResources() {
            BindingList<CustomResource> resources = new BindingList<CustomResource>();
            resources.Add(CreateCustomResource(1, "Max Fowler"));
            resources.Add(CreateCustomResource(2, "Nancy Drewmore"));
            resources.Add(CreateCustomResource(3, "Pak Jang"));
            return resources;
        }

        private CustomResource CreateCustomResource(int res_id, string caption) {
            CustomResource cr = new CustomResource();
            cr.ResID = res_id;
            cr.Name = caption;
            return cr;
        }

        public Random RandomInstance = new Random();
        private CustomAppointment CreateCustomAppointment(string subject, object resourceId, int status, int label, int fildIndex) {
            CustomAppointment apt = new CustomAppointment();
            apt.Subject = subject;
            apt.OwnerId = resourceId;
            Random rnd = RandomInstance;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            apt.CustomField = customFieldValues[fildIndex];
            return apt;
        }

        protected void ObjectDataSourceAppointment_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
            if(Session["CustomAppointmentDataSource"] == null) {
                Session["CustomAppointmentDataSource"] = new CustomAppointmentDataSource(GetCustomAppointments());
            }
            e.ObjectInstance = Session["CustomAppointmentDataSource"];
        }

        BindingList<CustomAppointment> GetCustomAppointments() {
            for(int i = 0; i < 5; i++) {
                customFieldValues.Add("Custom value " + i.ToString());
            }

            BindingList<CustomAppointment> appointments = new BindingList<CustomAppointment>();;
            CustomResourceDataSource resources = Session["CustomResourceDataSource"] as CustomResourceDataSource;
            if(resources != null) {
                foreach(CustomResource item in resources.Resources) {
                    string subjPrefix = item.Name + "'s ";
                    appointments.Add(CreateCustomAppointment(subjPrefix + "meeting", item.ResID, 2, 5, 1));
                    appointments.Add(CreateCustomAppointment(subjPrefix + "travel", item.ResID, 3, 6, 2));
                    appointments.Add(CreateCustomAppointment(subjPrefix + "phone call", item.ResID, 0, 10, 3));                       
                }                    
            }
            return appointments;
        }

        protected void ASPxScheduler1_PopupMenuShowing(object sender, DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs e) {
            if(e.Menu.MenuId == DevExpress.XtraScheduler.SchedulerMenuItemId.AppointmentMenu) {
                e.Menu.ClientInstanceName = "AppointmentPopupMenu";
                e.Menu.ClientSideEvents.PopUp = "OnClientPopupMenuShowing";
                e.Menu.ClientSideEvents.ItemClick = "OnClientItemClick";
                DevExpress.Web.MenuItem newItem = new DevExpress.Web.MenuItem();
                newItem.Name = "CustomValues";
                newItem.Text = "Custom values";
                int i = 0;
                foreach(string customFieldValue in customFieldValues) {
                    DevExpress.Web.MenuItem subMenuItem = new DevExpress.Web.MenuItem();
                    subMenuItem.Name = "Custom Item " + i.ToString();
                    i++;
                    subMenuItem.GroupName = "CustomValues";
                    subMenuItem.Text = customFieldValue;
                    subMenuItem.ClientEnabled = true;
                    newItem.Items.Add(subMenuItem);    
                }
                e.Menu.Items.Add(newItem);
            }
        }

        protected void ASPxScheduler1_InitClientAppointment(object sender, DevExpress.Web.ASPxScheduler.InitClientAppointmentEventArgs args) {
            args.Properties.Add("CustomFieldValue", args.Appointment.CustomFields["ApptCustomField"]);
        }

        protected void ASPxScheduler1_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e) {
            if (ASPxScheduler1.SelectedAppointments.Count > 0)
	        {
                ASPxScheduler1.SelectedAppointments[0].CustomFields["ApptCustomField"] = e.Parameter;
	        }            
        }

        protected void ASPxScheduler1_InitAppointmentDisplayText(object sender, DevExpress.XtraScheduler.AppointmentDisplayTextEventArgs e) {
            e.Text = e.Appointment.Subject;
            object customFieldValue = e.Appointment.CustomFields["ApptCustomField"];
            if(customFieldValue != null) {
                e.Text += " (custom field: " + customFieldValue.ToString() + ")";
            }
        }
    }
}