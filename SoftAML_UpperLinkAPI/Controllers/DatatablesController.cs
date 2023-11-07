using SoftAML_UpperLinkAPI.Extensions;
using SoftAML_UpperLinkAPI.Models.AuxillaryModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SoftAML_UpperLinkAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DatatablesController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> LoadAccountInformation([FromBody] DtParameters dtParameters)
        {
            try
            {
                var searchBy = dtParameters.Search?.Value;

                var orderCriteria = string.Empty;
                var orderAscendingDirection = true;

                if (dtParameters.Order != null)
                {
                    // in this example we just default sort on the 1st column
                    orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                    orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                }
                else
                {
                    // if we have an empty search then just order the results by Id ascending
                    orderCriteria = "Id";
                    orderAscendingDirection = true;
                }
                using (SoftAML_UpperLinkAPI.Contexts.Entities _db = new Contexts.Entities())
                {

                    _db.Database.CommandTimeout = 0;
                    var result = _db.account_information_upload.Where(x=>true);

                    if (!string.IsNullOrEmpty(searchBy))
                    {
                        result = result.Where(r => r.account_balance != null && r.account_balance.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.account_beneficiary != null && r.account_beneficiary.ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.account_branch != null && r.account_branch.ToUpper().Contains(searchBy.ToUpper()) ||
                                                   //r.account_closed_date != null && r.account_closed_date.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.account_currency_code != null && r.account_currency_code.ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.account_name != null && r.account_name.ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.account_number != null && r.account_number.ToUpper().Contains(searchBy.ToUpper()) ||
                                                   //r.account_opened_date != null && r.account_opened_date.ToString().ToUpper().Contains(searchBy.ToUpper())
                                                   r.account_type != null && r.account_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.batch_number != null && r.batch_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.client_number != null && r.client_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_address != null && r.entity_address.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_address_type != null && r.entity_address_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_business != null && r.entity_business.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_city != null && r.entity_city.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_country_code != null && r.entity_country_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_email != null && r.entity_email.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_incorporation_number != null && r.entity_incorporation_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_name != null && r.entity_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_phone_contact_type != null && r.entity_phone_contact_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_phone_number != null && r.entity_phone_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_phone_type != null && r.entity_phone_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.entity_state != null && r.entity_state.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.uploaded != null && r.uploaded.ToString().ToUpper().Contains(searchBy.ToUpper())

                                                   );
                    }

                    result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

                    // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                    var filteredResultsCount = await result.LongCountAsync();
                    var totalResultsCount = await _db.account_information_upload.CountAsync();

                    return Json(new
                    {
                        draw = dtParameters.Draw,
                        recordsTotal = totalResultsCount,
                        recordsFiltered = filteredResultsCount,
                        data = await result
                            .Skip(dtParameters.Start)
                            .Take(dtParameters.Length)
                            .ToListAsync()
                    });
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            
        }
        [HttpPost]
        public async Task<IHttpActionResult> LoadAccountSignatoryInformation([FromBody] DtParameters dtParameters) 
        {
            try
            {
                var searchBy = dtParameters.Search?.Value;

                var orderCriteria = string.Empty;
                var orderAscendingDirection = true;

                if (dtParameters.Order != null)
                {
                    // in this example we just default sort on the 1st column
                    orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                    orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                }
                else
                {
                    // if we have an empty search then just order the results by Id ascending
                    orderCriteria = "Id";
                    orderAscendingDirection = true;
                }
                using (SoftAML_UpperLinkAPI.Contexts.Entities _db = new Contexts.Entities())
                {

                    _db.Database.CommandTimeout = 0;
                    var result = _db.account_signatories_directors_upload.Where(x=>true);

                    if (!string.IsNullOrEmpty(searchBy))
                    {
                        result = result.Where(r =>
                                                   r.account_number != null && r.account_number.ToUpper().Contains(searchBy.ToUpper()) ||
                                                   //r.account_opened_date != null && r.account_opened_date.ToString().ToUpper().Contains(searchBy.ToUpper())
                                                   r.client_number != null && r.client_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.batch_number != null && r.batch_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_type != null && r.client_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.is_primary != null && r.is_primary.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_title != null && r.person_title.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_first_name != null && r.person_first_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_last_name != null && r.person_last_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_gender != null && r.person_gender.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_birth_date != null && r.person_birth_date.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_passport_number != null && r.person_passport_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_passport_country != null && r.person_passport_country.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_id_type != null && r.person_id_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_id_number != null && r.person_id_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_issue_country != null && r.person_issue_country.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_nationality != null && r.person_nationality.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_tph_contact_type != null && r.person_tph_contact_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_tph_comm_type != null && r.person_tph_comm_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_phone_number != null && r.person_phone_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_address_type != null && r.person_address_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_address != null && r.person_address.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_city != null && r.person_city.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_state != null && r.person_state.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_country_code != null && r.person_country_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_email != null && r.person_email.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_occupation != null && r.person_occupation.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.person_tax_number != null && r.person_tax_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   //r.expiry_date != null && r.expiry_date.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   //r.issues_country != null && r.issues_country.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   //r.issue_date != null && r.issue_date.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   //r.issued_by != null && r.issued_by.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   //r.id_type != null && r.id_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                                   r.uploaded != null && r.uploaded.ToString().ToUpper().Contains(searchBy.ToUpper())

                                                   );
                    }

                    result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

                    // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                    var filteredResultsCount = await result.LongCountAsync();
                    var totalResultsCount = await _db.account_signatories_directors_upload.LongCountAsync();

                    return Json(new
                    {
                        draw = dtParameters.Draw,
                        recordsTotal = totalResultsCount,
                        recordsFiltered = filteredResultsCount,
                        data = await result
                            .Skip(dtParameters.Start)
                            .Take(dtParameters.Length)
                            .ToListAsync()
                    });
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            
        }


        [HttpPost]
        public async Task<IHttpActionResult> LoadTransactionInformation([FromBody] DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }
            using (SoftAML_UpperLinkAPI.Contexts.Entities _db = new Contexts.Entities())
            {

                _db.Database.CommandTimeout = 0;
                var result = _db.transaction_information_upload.Where(x=>true);

                if (!string.IsNullOrEmpty(searchBy))
                {
                    result = result.Where(r =>
                                               r.t_account_number != null && r.t_account_number.ToUpper().Contains(searchBy.ToUpper()) ||
                                               //r.account_opened_date != null && r.account_opened_date.ToString().ToUpper().Contains(searchBy.ToUpper())
                                               r.t_trans_number != null && r.t_trans_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.batch_number != null && r.batch_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_location != null && r.t_location.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.transaction_description != null && r.transaction_description.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_date != null && r.t_date.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_teller != null && r.t_teller.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_authorized != null && r.t_authorized.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_late_deposit != null && r.t_late_deposit.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_date_posting != null && r.t_date_posting.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_value_date != null && r.t_value_date.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_transmode_code != null && r.t_transmode_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_amount_local != null && r.t_amount_local.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_client_type != null && r.t_source_client_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_type != null && r.t_source_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_funds_code != null && r.t_source_funds_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_currency_code != null && r.t_source_currency_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_foreign_amount != null && r.t_source_foreign_amount.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_exchange_rate != null && r.t_source_exchange_rate.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_country != null && r.t_source_country.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_institution_code != null && r.t_source_institution_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_institution_name != null && r.t_source_institution_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_account_number != null && r.t_source_account_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_account_name != null && r.t_source_account_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_person_first_name != null && r.t_source_person_first_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_person_last_name != null && r.t_source_person_last_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_source_entity_name != null && r.t_source_entity_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_client_type != null && r.t_dest_client_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_type != null && r.t_dest_type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_funds_code != null && r.t_dest_funds_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_currency_code != null && r.t_dest_currency_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_foreign_amount != null && r.t_dest_foreign_amount.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_exchange_rate != null && r.t_dest_exchange_rate.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_country != null && r.t_dest_country.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_institution_code != null && r.t_dest_institution_code.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_institution_name != null && r.t_dest_institution_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_account_number != null && r.t_dest_account_number.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_account_name != null && r.t_dest_account_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_person_first_name != null && r.t_dest_person_first_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_person_last_name != null && r.t_dest_person_last_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.t_dest_entity_name != null && r.t_dest_entity_name.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.Tran_Type != null && r.Tran_Type.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.uploaded != null && r.uploaded.ToString().ToUpper().Contains(searchBy.ToUpper())

                                               );
                }

                result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

                // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                var filteredResultsCount = await result.LongCountAsync();
                var totalResultsCount = await _db.transaction_information_upload.LongCountAsync();

                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = await result
                        .Skip(dtParameters.Start)
                        .Take(dtParameters.Length)
                        .ToListAsync()
                });
            }
        }
    }
}
