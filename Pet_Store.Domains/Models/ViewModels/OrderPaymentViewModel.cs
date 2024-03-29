﻿using Pet_Store.Domains.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class OrderPaymentViewModel
    {
        public Payments payments { get; set; }

        public OrderHeader orderHeader { get; set; }

        public List<OrderDetails> order { get; set; }

        public List<Products> products { get; set; }

        public string UserId { get; set; }

        public int Response { get; set; }

        public double Total { get; set; }
    }
}
