//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftAML_UpperLinkAPI.Contexts
{
    using System;
    using System.Collections.Generic;
    
    public partial class account_signatories_directors_upload
    {
        public long id { get; set; }
        public string account_number { get; set; }
        public string client_number { get; set; }
        public string person_type { get; set; }
        public string is_primary { get; set; }
        public string person_title { get; set; }
        public string person_first_name { get; set; }
        public string person_last_name { get; set; }
        public string person_gender { get; set; }
        public Nullable<System.DateTime> person_birth_date { get; set; }
        public string person_passport_number { get; set; }
        public string person_passport_country { get; set; }
        public string person_id_type { get; set; }
        public string person_id_number { get; set; }
        public string person_issue_country { get; set; }
        public string person_nationality { get; set; }
        public string person_tph_contact_type { get; set; }
        public string person_tph_comm_type { get; set; }
        public string person_phone_number { get; set; }
        public string person_address_type { get; set; }
        public string person_address { get; set; }
        public string person_city { get; set; }
        public string person_state { get; set; }
        public string person_country_code { get; set; }
        public string person_email { get; set; }
        public string person_occupation { get; set; }
        public string person_tax_number { get; set; }
        public string uploader { get; set; }
        public Nullable<System.DateTime> uploaded { get; set; }
        public string batch_number { get; set; }
    }
}