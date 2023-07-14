﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishi.Utility
{
    public class SD
    {
        public const string ManagerRole = "Manager";
        public const string FornDeskRole = "Front";
        public const string KitchenRole = "KitchenRole";
        public const string CustomerRole = "CustomerRole";

		public const string StatusPending = "Pending_Payment";
		public const string StatusSubmitted = "Submitted_PaymentApproved";
		public const string StatusRejected = "Rejected_Payment";
		public const string StatusInProcess = "Being Prepared";
		public const string StatusReady = "Ready for Pickup";
		public const string StatusCompleted = "Completed";
		public const string StatusCancelled = "Cancelled";
		public const string StatusRefund = "Refunded";

        //Session key
        public const string SessionCart = "SessionCart";
    }
}
