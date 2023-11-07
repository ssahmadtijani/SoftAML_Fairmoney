using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SoftAML_UpperLinkAPI.Models
{
    /// <summary>
    /// Transaction Information Description
    /// </summary>
    public  class transaction_information_uploads_model
    {
        /// <summary>
        /// Transaction Account Number
        /// </summary>
        public string t_account_number { get; set; }
        /// <summary>
        /// Unique transaction number for bank transaction
        /// </summary>
        public string t_trans_number { get; set; }
        /// <summary>
        /// Branch/Location where the transaction took place
        /// </summary>
        public string t_location { get; set; }
        /// <summary>
        /// Description of the transaction
        /// </summary>
        public string transaction_description { get; set; }
        /// <summary>
        /// Date and time of the transaction
        /// </summary>
        public Nullable<System.DateTime> t_date { get; set; }
        /// <summary>
        /// Who conducted the transaction
        /// </summary>
        public string t_teller { get; set; }
        /// <summary>
        /// Who authorized the transaction
        /// </summary>
        public string t_authorized { get; set; }
        /// <summary>
        /// Late deposit indicator 
        /// 1 for late deposit otherwise 0
        /// </summary>
        public string t_late_deposit { get; set; }
        /// <summary>
        /// Date of posting (if different from date of transaction) 
        /// </summary>
        public Nullable<System.DateTime> t_date_posting { get; set; }
        /// <summary>
        /// Transaction Value Date and Time
        /// </summary>
        public Nullable<System.DateTime> t_value_date { get; set; }
        /// <summary>
        /// How the transaction was conducted
        /// A-In-branch/Office, B-ATM, C-Electronic transaction
        /// </summary>
        public string t_transmode_code { get; set; }
        /// <summary>
        /// The value of the transaction in local currency
        /// </summary>
        public Nullable<decimal> t_amount_local { get; set; }
        /// <summary>
        /// Specifies where the money came from.
        /// varchar(1) [0,1] : 0 = Not My Client, 1 = My Client 
        /// </summary>
        public string t_source_client_type { get; set; }
        /// <summary>
        /// Specifies the source of the money either account, person or entity
        /// </summary>
        public string t_source_type { get; set; }
        /// <summary>
        /// Type of funds used in initiating transaction
        /// varchar(50) A-Deposit, B-Electronic Funds Transfer, E-Bank Draft, FManager’s Cheque, G-Traveler’s cheque, H-Life insurance policy, KCash, L-From Account, O-Other, PCheque
        /// </summary>
        public string t_source_funds_code { get; set; }
        /// <summary>
        /// Local Currency code 
        /// Varchar(3) e.g NGN
        /// </summary>
        public string t_source_currency_code { get; set; }
        /// <summary>
        /// If the transaction is conducted in foreign currency, then specify the foreign currency details.
        /// </summary>
        public Nullable<decimal> t_source_foreign_amount { get; set; }
        /// <summary>
        /// Exchange rate which has been used for transaction
        /// </summary>
        public Nullable<decimal> t_source_exchange_rate { get; set; }
        /// <summary>
        /// The country the source of money come from
        /// </summary>
        public string t_source_country { get; set; }
        /// <summary>
        /// Institution Code
        /// </summary>
        public string t_source_institution_code { get; set; }
        /// <summary>
        ///  Source Institution Name
        /// </summary>
        public string t_source_institution_name { get; set; }
        /// <summary>
        /// The Source Account Number
        /// </summary>
        public string t_source_account_number { get; set; }
        /// <summary>
        /// The source account name
        /// </summary>
        public string t_source_account_name { get; set; }
        /// <summary>
        /// First name of the source person 
        /// </summary>
        public string t_source_person_first_name { get; set; }
        /// <summary>
        /// Last name of the source person 
        /// </summary>
        public string t_source_person_last_name { get; set; }
        /// <summary>
        /// Name of source entity
        /// </summary>
        public string t_source_entity_name { get; set; }
        /// <summary>
        /// Specifies where the money go to 
        /// varchar(1) [0,1] : 0 = Not My Client, 1 = My Client 
        /// </summary>
        public string t_dest_client_type { get; set; }
        /// <summary>
        /// Specifies where the money goes to, either account, person or entity
        /// </summary>
        public string t_dest_type { get; set; }
        /// <summary>
        /// Type of funds used in initiating transaction
        /// varchar(50) A-Deposit, B-Electronic Funds Transfer, E-Bank Draft, FManager’s Cheque, G-Traveller’s cheque, H-Life insurance policy, KCash, L-From Account, O-Other, PCheque
        /// </summary>
        public string t_dest_funds_code { get; set; }
        /// <summary>
        /// Local Currency code
        /// varchar(3) e.g NGN
        /// </summary>
        public string t_dest_currency_code { get; set; }
        /// <summary>
        /// If the transaction is conducted in foreign currency, then specify the foreign currency details.
        /// </summary>
        public Nullable<decimal> t_dest_foreign_amount { get; set; }
        /// <summary>
        /// Exchange rate which has been used for transaction
        /// </summary>
        public Nullable<decimal> t_dest_exchange_rate { get; set; }
        /// <summary>
        /// The country the money goes to
        /// </summary>
        public string t_dest_country { get; set; }
        /// <summary>
        /// Destination Institution Code
        /// </summary>
        public string t_dest_institution_code { get; set; }
        /// <summary>
        /// Destination Institution
        /// </summary>
        public string t_dest_institution_name { get; set; }
        /// <summary>
        /// The destiantion account number
        /// </summary>
        public string t_dest_account_number { get; set; }
        /// <summary>
        /// The destination account name
        /// </summary>
        public string t_dest_account_name { get; set; }
        /// <summary>
        /// First name of the destination person
        /// </summary>
        public string t_dest_person_first_name { get; set; }
        /// <summary>
        /// Last name of the the destination person
        /// </summary>
        public string t_dest_person_last_name { get; set; }
        /// <summary>
        /// Name of destination entity
        /// </summary>
        public string t_dest_entity_name { get; set; }
        /// <summary>
        /// Transaction Type
        /// NEFT INWARD, NIPS INWARD e.t.c..
        /// </summary>
        public string Tran_Type { get; set; }
        /// <summary>
        /// Uploader
        /// </summary>
        [IgnoreDataMember]
        public string uploader { get; set; }
        /// <summary>
        /// Uploaded
        /// </summary>
        [IgnoreDataMember]
        public Nullable<System.DateTime> uploaded { get;  set; }
        /// <summary>
        /// Batch Number
        /// </summary>
        [IgnoreDataMember]
        public string batch_number { get;  set; }
        
    }
    /// <summary>
    /// Account Information Description
    /// </summary>
    public class account_information_uploads_model
    {
        /// <summary>
        /// Account Number
        /// </summary>
        public string account_number { get; set; }
        /// <summary>
        /// Branch holding account
        /// </summary>
        public string account_branch { get; set; }
        /// <summary>
        /// Account Name
        /// </summary>
        public string account_name { get; set; }
        /// <summary>
        /// The account holder BVN
        /// </summary>
        public string client_number { get; set; }
        /// <summary>
        /// Currency the account is kept in
        /// </summary>
        public string account_currency_code { get; set; }
        /// <summary>
        /// account Type Code
        /// varchar(1) A_Business, C_Savings, D_Current Business, E_Current personal, I_Fixed, L_Trading Account, O_Other, P_Dormicilliary
        /// </summary>
        public string account_type { get; set; }
        /// <summary>
        /// Date Account was opened
        /// </summary>
        public Nullable<System.DateTime> account_opened_date { get; set; }
        /// <summary>
        /// Date account was closed, if it has been closed.
        /// </summary>
        public string account_closed_date { get; set; }
        /// <summary>
        /// account Status after transaction was conducted
        /// varchar(1) A_Active, B_Partially Blocked (PND), C_Dormant, H_Fully Blocked, P_performing, N_Non-Performin
        /// </summary>
        public string account_status_code { get; set; }
        /// <summary>
        /// Ultimate beneficiary of the account
        /// </summary>
        public string account_beneficiary { get; set; }
        /// <summary>
        /// The account balance after the transaction was conducted
        /// </summary>
        public Nullable<decimal> account_balance { get; set; }
        /// <summary>
        /// Involved entity name, If Any
        /// </summary>
        public string entity_name { get; set; }
        /// <summary>
        /// The registration number of the entity/"company" in the relevant authority(e.g.Chamber ofCommerce)
        /// </summary>
        public string entity_incorporation_number { get; set; }
        /// <summary>
        /// Business of the entity 
        /// </summary>
        public string entity_business { get; set; }
        /// <summary>
        /// Business of the entity 
        /// varchar(10) Business - B, Private - P
        /// </summary>
        public string entity_phone_contact_type { get; set; }
        /// <summary>
        /// Entity Telephone type
        /// Enumeration L-Landline Phone, MMobile Phone, F-Fax, S-Satellite Phone, P-Pager, E-Email
        /// </summary>
        public string entity_phone_type { get; set; }
        /// <summary>
        /// Entity Telephone number
        /// </summary>
        public string entity_phone_number { get; set; }
        /// <summary>
        /// The contact type of the address
        /// Street name and house number
        /// </summary>
        public string entity_address_type { get; set; }
        /// <summary>
        /// Street name and house number
        /// </summary>
        public string entity_address { get; set; }
        /// <summary>
        /// City where the entity can be located 
        /// </summary>
        public string entity_city { get; set; }
        /// <summary>
        /// City where the entity can be located 
        /// </summary>
        public string entity_state { get; set; }
        /// <summary>
        /// Country Code of the enitity
        /// varchar(2) e.g NG
        /// </summary>
        public string entity_country_code { get; set; }
        /// <summary>
        /// Email address of the entity 
        /// </summary>
        public string entity_email { get; set; }
        /// <summary>
        /// Uploader
        /// </summary>
        [IgnoreDataMember]
        public string uploader { get; set; }
        /// <summary>
        /// Date Uploaded
        /// </summary>
        [IgnoreDataMember]
        public Nullable<System.DateTime> uploaded { get; set; }
        /// <summary>
        /// Batch Number
        /// </summary>
        [IgnoreDataMember]
        public string batch_number { get; set; }
        /// <summary>
        /// Account Signatories
        /// </summary>
        public List<account_signatories_directors_uploads_model> signatories { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public account_information_uploads_model()
        {
            signatories = new List<account_signatories_directors_uploads_model>();
        }
    }
    /// <summary>
    /// Account Signatories and Directors
    /// </summary>
    public class account_signatories_directors_uploads_model
    {
        //public long id { get; set; }
        /// <summary>
        /// Account Number
        /// </summary>
        public string account_number { get; set; }
        /// <summary>
        /// The account holder BVN
        /// </summary>
        public string client_number { get; set; }
        /// <summary>
        /// Cooperate or Individual 
        /// </summary>
        public string person_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_primary { get; set; }
        /// <summary>
        /// Title of the signatory Person(s) with access to the account
        /// </summary>
        public string person_title { get; set; }
        /// <summary>
        /// First name of Person(s) with access to the account.
        /// </summary>
        public string person_first_name { get; set; }
        /// <summary>
        /// Last name of Person(s) with access to the account.
        /// </summary>
        public string person_last_name { get; set; }
        /// <summary>
        /// Signatory's gender
        /// </summary>
        public string person_gender { get; set; }
        /// <summary>
        /// Signatory's gender
        /// </summary>
        public Nullable<System.DateTime> person_birth_date { get; set; }
        /// <summary>
        /// Signatory's Passport number 
        /// </summary>
        public string person_passport_number { get; set; }
        /// <summary>
        /// Passport issue country (Can be reported only when there is a passport number)
        /// </summary>
        public string person_passport_country { get; set; }
        /// <summary>
        /// -Driver's license, B-National Identity Card, BNational Identity Card, C-Passport, E-Voter’s Registration Card, R-RC No, O-3rd Party ID
        /// </summary>
        public string person_id_type { get; set; }
        /// <summary>
        /// Any additional identification number rather than ssn and passpor
        /// </summary>
        public string person_id_number { get; set; }
        /// <summary>
        /// Country where the identification number was issued
        /// </summary>
        public string person_issue_country { get; set; }
        /// <summary>
        /// Country of Nationality
        /// </summary>
        public string person_nationality { get; set; }
        /// <summary>
        /// Private (B), Business (B)
        /// </summary>
        public string person_tph_contact_type { get; set; }
        /// <summary>
        /// Mobile (M), Fax (F), Landline (L) 
        /// </summary>
        public string person_tph_comm_type { get; set; }
        /// <summary>
        /// Phone number(s) of the signatory to the account
        /// </summary>
        public string person_phone_number { get; set; }
        /// <summary>
        /// Private (B), Business (B)
        /// </summary>
        public string person_address_type { get; set; }
        /// <summary>
        /// Location description of the signatory
        /// </summary>
        public string person_address { get; set; }
        /// <summary>
        /// City of the signatory 
        /// </summary>
        public string person_city { get; set; }
        /// <summary>
        /// State of the signatory
        /// </summary>
        public string person_state { get; set; }
        /// <summary>
        /// Country of the signatory
        /// </summary>
        public string person_country_code { get; set; }
        /// <summary>
        /// Email address of the signatory
        /// </summary>
        public string person_email { get; set; }
        /// <summary>
        /// Occupation of the signatory
        /// </summary>
        public string person_occupation { get; set; }
        /// <summary>
        /// Signatory tax number
        /// </summary>
        public string person_tax_number { get; set; }
        /// <summary>
        /// ID Card Expiry date
        /// </summary>
        public Nullable<System.DateTime> expiry_date { get; set; }
        /// <summary>
        /// COuntry where passport was issued
        /// </summary>
        public string issues_country { get; set; }
        /// <summary>
        /// Date passport was issued
        /// </summary>
        public Nullable<System.DateTime> issue_date { get; set; }
        /// <summary>
        /// Agency that issued passport
        /// </summary>
        public string issued_by { get; set; }
        /// <summary>
        /// -Driver's license, B-National Identity Card, BNational Identity Card, C-Passport, E-Voter’s Registration Card, R-RC No, O-3rd Party ID
        /// </summary>
        public string id_type { get; set; }
        /// <summary>
        /// Uploader
        /// </summary>
        [IgnoreDataMember]
        public string uploader { get; set; }
        /// <summary>
        /// Uploaded
        /// </summary>
        [IgnoreDataMember]
        public Nullable<System.DateTime> uploaded { get; set; }
        /// <summary>
        /// Batch number
        /// </summary>
        [IgnoreDataMember]
        public string batch_number { get; set; }
    }


}