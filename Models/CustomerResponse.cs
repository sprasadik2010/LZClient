using System;
using System.Collections.Generic;

namespace LZClient.Models
{
    public class PaymentResponse
    {
        public bool success { get; set; }
        public string responseCode { get; set; }
        public string message { get; set; }
        public List<PaymentDefinition> paymentDefinitions { get; set; }
        public List<AllPayment> allPayments { get; set; }
        public List<string> errors { get; set; }
    }

    public class PaymentDefinition
    {
        public int scheduledPaymentID { get; set; }
        public string customerRef { get; set; }
        public string customerName { get; set; }
        public decimal firstPaymentAmount { get; set; }
        public DateTime? firstPaymentDate { get; set; }
        public decimal regularPaymentAmount { get; set; }
        public DateTime regularPaymentStartDate { get; set; }
        public int regularPaymentFrequency { get; set; }
        public int numberOfRegularPayments { get; set; }
        public DateTime? nextPaymentDate { get; set; }
        public decimal nextAmount { get; set; }
        public DateTime? lastPaymentDate { get; set; }
        public decimal lastAmount { get; set; }
        public int numberOfPaymentsReceived { get; set; }
        public DateTime scheduleSetupDate { get; set; }

        // For display in grid
        public string Forename
        {
            get
            {
                if (!string.IsNullOrEmpty(customerName))
                {
                    var parts = customerName.Split(' ');
                    return parts.Length > 1 ? parts[1] : customerName;
                }
                return "";
            }
        }

        public string Surname
        {
            get
            {
                if (!string.IsNullOrEmpty(customerName))
                {
                    var parts = customerName.Split(' ');
                    return parts.Length > 1 ? string.Join(" ", parts, 2, parts.Length - 2) : "";
                }
                return "";
            }
        }

        public string Title
        {
            get
            {
                if (!string.IsNullOrEmpty(customerName))
                {
                    var parts = customerName.Split(' ');
                    return parts.Length > 0 ? parts[0] : "";
                }
                return "";
            }
        }

        public string SO_Ref_No
        {
            get { return customerRef; }
        }

        public decimal Regular_Amount
        {
            get { return regularPaymentAmount; }
        }

        public string Item_Next_Collection_Date
        {
            get
            {
                return nextPaymentDate.HasValue
                    ? nextPaymentDate.Value.ToString("dd/MM/yyyy")
                    : "";
            }
        }

        // For compatibility with existing grid columns
        public string DOB
        {
            get { return ""; } // API doesn't provide DOB
        }

        public string PostCode
        {
            get { return ""; } // API doesn't provide postcode
        }

        public string Mobile
        {
            get { return ""; } // API doesn't provide mobile
        }

        public string Action
        {
            get { return "Print"; }
        }
    }

    public class AllPayment
    {
        public string customerRef { get; set; }
        public DateTime collectionDate { get; set; }
        public decimal amount { get; set; }
    }
}