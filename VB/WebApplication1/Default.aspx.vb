Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace WebApplication1
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Private customFieldValues As New List(Of String)()
        Protected Sub ObjectDataSourceResources_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
            If Session("CustomResourceDataSource") Is Nothing Then
                Session("CustomResourceDataSource") = New CustomResourceDataSource(GetCustomResources())
            End If
            e.ObjectInstance = Session("CustomResourceDataSource")
        End Sub

        Private Function GetCustomResources() As BindingList(Of CustomResource)
            Dim resources As New BindingList(Of CustomResource)()
            resources.Add(CreateCustomResource(1, "Max Fowler"))
            resources.Add(CreateCustomResource(2, "Nancy Drewmore"))
            resources.Add(CreateCustomResource(3, "Pak Jang"))
            Return resources
        End Function

        Private Function CreateCustomResource(ByVal res_id As Integer, ByVal caption As String) As CustomResource
            Dim cr As New CustomResource()
            cr.ResID = res_id
            cr.Name = caption
            Return cr
        End Function

        Public RandomInstance As New Random()
        Private Function CreateCustomAppointment(ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer, ByVal fildIndex As Integer) As CustomAppointment
            Dim apt As New CustomAppointment()
            apt.Subject = subject
            apt.OwnerId = resourceId
            Dim rnd As Random = RandomInstance
            Dim rangeInMinutes As Integer = 60 * 24
            apt.StartTime = Date.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime.Add(TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4)))
            apt.Status = status
            apt.Label = label
            apt.CustomField = customFieldValues(fildIndex)
            Return apt
        End Function

        Protected Sub ObjectDataSourceAppointment_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
            If Session("CustomAppointmentDataSource") Is Nothing Then
                Session("CustomAppointmentDataSource") = New CustomAppointmentDataSource(GetCustomAppointments())
            End If
            e.ObjectInstance = Session("CustomAppointmentDataSource")
        End Sub

        Private Function GetCustomAppointments() As BindingList(Of CustomAppointment)
            For i As Integer = 0 To 4
                customFieldValues.Add("Custom value " & i.ToString())
            Next i

            Dim appointments As New BindingList(Of CustomAppointment)()
            Dim resources As CustomResourceDataSource = TryCast(Session("CustomResourceDataSource"), CustomResourceDataSource)
            If resources IsNot Nothing Then
                For Each item As CustomResource In resources.Resources
                    Dim subjPrefix As String = item.Name & "'s "
                    appointments.Add(CreateCustomAppointment(subjPrefix & "meeting", item.ResID, 2, 5, 1))
                    appointments.Add(CreateCustomAppointment(subjPrefix & "travel", item.ResID, 3, 6, 2))
                    appointments.Add(CreateCustomAppointment(subjPrefix & "phone call", item.ResID, 0, 10, 3))
                Next item
            End If
            Return appointments
        End Function

        Protected Sub ASPxScheduler1_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs)
            If e.Menu.MenuId = DevExpress.XtraScheduler.SchedulerMenuItemId.AppointmentMenu Then
                e.Menu.ClientInstanceName = "AppointmentPopupMenu"
                e.Menu.ClientSideEvents.PopUp = "OnClientPopupMenuShowing"
                e.Menu.ClientSideEvents.ItemClick = "OnClientItemClick"
                Dim newItem As New DevExpress.Web.MenuItem()
                newItem.Name = "CustomValues"
                newItem.Text = "Custom values"
                Dim i As Integer = 0
                For Each customFieldValue As String In customFieldValues
                    Dim subMenuItem As New DevExpress.Web.MenuItem()
                    subMenuItem.Name = "Custom Item " & i.ToString()
                    i += 1
                    subMenuItem.GroupName = "CustomValues"
                    subMenuItem.Text = customFieldValue
                    subMenuItem.ClientEnabled = True
                    newItem.Items.Add(subMenuItem)
                Next customFieldValue
                e.Menu.Items.Add(newItem)
            End If
        End Sub

        Protected Sub ASPxScheduler1_InitClientAppointment(ByVal sender As Object, ByVal args As DevExpress.Web.ASPxScheduler.InitClientAppointmentEventArgs)
            args.Properties.Add("CustomFieldValue", args.Appointment.CustomFields("ApptCustomField"))
        End Sub

        Protected Sub ASPxScheduler1_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase)
            If ASPxScheduler1.SelectedAppointments.Count > 0 Then
                ASPxScheduler1.SelectedAppointments(0).CustomFields("ApptCustomField") = e.Parameter
            End If
        End Sub

        Protected Sub ASPxScheduler1_InitAppointmentDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.AppointmentDisplayTextEventArgs)
            e.Text = e.Appointment.Subject
            Dim customFieldValue As Object = e.Appointment.CustomFields("ApptCustomField")
            If customFieldValue IsNot Nothing Then
                e.Text &= " (custom field: " & customFieldValue.ToString() & ")"
            End If
        End Sub
    End Class
End Namespace