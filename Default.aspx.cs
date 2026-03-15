using LZClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LZClient
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] + "/api/PaymentAndCollection/get-scheduled-active-payments";
        private List<PaymentDefinition> payments = new List<PaymentDefinition>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Register the async task
                RegisterAsyncTask(new PageAsyncTask(LoadDataAsync));
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                payments = await GetPaymentsFromApi().ConfigureAwait(false);
                BindData();
            }
            catch (Exception ex)
            {
                // Handle error
                Response.Write($"<script>alert('Error loading data: {ex.Message.Replace("'", "\\'")}');</script>");
            }
        }

        private async Task<List<PaymentDefinition>> GetPaymentsFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                var requestData = new
                {
                    showFuturePayments = true,
                    startDate = "2025-01-01",
                    endDate = "2026-03-30"
                };

                string jsonContent = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var apiResponse = JsonConvert.DeserializeObject<PaymentResponse>(jsonResponse);

                    if (apiResponse != null && apiResponse.success)
                    {
                        return apiResponse.paymentDefinitions ?? new List<PaymentDefinition>();
                    }
                }

                return new List<PaymentDefinition>();
            }
        }

        private void BindData()
        {
            gvCustomers.DataSource = payments;
            gvCustomers.DataBind();
        }

        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomers.PageIndex = e.NewPageIndex;
            RegisterAsyncTask(new PageAsyncTask(LoadDataAsync));
        }

        // For async event handlers - async void is acceptable for event handlers
        protected async void lnkPrint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string scheduledPaymentID = btn.CommandArgument;

            // Handle print action - you can get more details from the payment object
            await Task.Delay(100); // Simulate async work if needed
            Response.Write($"<script>alert('Printing for Scheduled Payment ID: {scheduledPaymentID}');</script>");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Handle delete all action
            Response.Write("<script>alert('Delete all functionality');</script>");
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // Handle delete selected items
            List<string> selectedPayments = new List<string>();

            foreach (GridViewRow row in gvCustomers.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    string scheduledPaymentID = gvCustomers.DataKeys[row.RowIndex].Value.ToString();
                    selectedPayments.Add(scheduledPaymentID);
                }
            }

            if (selectedPayments.Count > 0)
            {
                Response.Write($"<script>alert('Deleting selected payments: {string.Join(", ", selectedPayments)}');</script>");
            }
            else
            {
                Response.Write("<script>alert('No items selected');</script>");
            }
        }
    }
}