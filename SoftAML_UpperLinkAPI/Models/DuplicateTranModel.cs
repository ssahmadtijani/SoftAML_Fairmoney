using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftAML_UpperLinkAPI.Models
{
    public class DuplicateTranModel
    {
        public string t_account_number { get; set; }
        public string t_trans_number { get; set; }
        public DateTime? t_date { get; set; }
        public decimal? t_amount_local { get; set; }
    }

    public class DuplicateAccountModel
    {
        public string account_number { get; set; }
        public string account_name { get; set; }
    }

    public class DuplicateSignatoryModel
    {
        public string account_number { get; set; }
        public string person_first_name { get; set; }
        public string person_last_name { get; set; }
        public string person_gender { get; set; }
        public DateTime? person_birth_date { get; set; }
    }
}