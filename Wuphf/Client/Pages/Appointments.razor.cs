using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Wuphf.Shared.Appointments;
using Wuphf.Client.Models;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Linq;
using System.ComponentModel;

namespace Wuphf.Client.Pages
{
	public partial class Appointments : ComponentBase
	{

        protected override async Task OnInitializedAsync()
        {
            Appts.PropertyChanged += (object sender, PropertyChangedEventArgs e) => StateHasChanged();
            await Appts.GetAppointments();
        }
        public void OnAdd()
        {
            Appts.OnAdd();
            StateHasChanged();
        }
        public void OnEdit(Appointment appt)
        {
            Appts.OnEdit(appt);
            StateHasChanged();
        }
        public async void OnRemove(Appointment appt)
        {
            await Appts.OnRemove(appt);
            StateHasChanged();
        }

        private async void HandleValidSubmit()
        {
            await Appts.SaveSelectedAppointment();
        }

    }
}

