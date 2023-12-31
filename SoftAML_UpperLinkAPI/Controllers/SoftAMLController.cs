﻿using SoftAML_UpperLinkAPI.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SoftAML_UpperLinkAPI.Models;
using AutoMapper;
using System.Data.Entity;
using System.IO;
using System.Configuration;
using Dapper;

namespace SoftAML_UpperLinkAPI.Controllers
{
    ///<summary>
    ///Soft AML Upload
    ///</summary>
    [Authorize]
    public class SoftAMLController : ApiController
    {
        private readonly DapperContext dapper;
        /// <summary>
        /// Controller Constructor
        /// </summary>
        public SoftAMLController()
        {
            dapper = new DapperContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        }
        [NonAction]
        private async Task WriteLog(Exception ex, string source, string payLoad, string folder)
        {
            /*using (FileStream stream = new FileStream(string.Concat($@"C:\SoftAMLFairMoney\APILogs\{folder}\", User.Identity.Name, "_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".log"), FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            using (StreamWriter sw = new StreamWriter(stream))
            {
                await sw.WriteLineAsync(string.Concat("Endpoint: ", source));
                await sw.WriteLineAsync("___________________________________________________________________________________________________");
                await sw.WriteLineAsync(string.Concat("PayLoad: ", payLoad));
                await sw.WriteLineAsync("___________________________________________________________________________________________________");
                await sw.WriteLineAsync(ex.ToString());
            }*/

            try
            {
                using (var connection = dapper.Connection)
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();
                    var mapped = new tblAPILog
                    {
                        Endpoint = source,
                        PayLoad = payLoad,
                        Message = ex.ToString()
                    };
                    var products = await connection.ExecuteAsync("INSERT INTO [dbo].[tblAPILogs]           ([Endpoint]           ,[PayLoad]           ,[Message])     VALUES           (@Endpoint           ,@PayLoad           ,@Message)", mapped);
                }
            }
            catch (Exception exx)
            {

                using (FileStream stream = new FileStream(string.Concat($@"C:\SoftAMLFairMoney\APILogs\{folder}\", User.Identity.Name, "_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".log"), FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    await sw.WriteLineAsync(string.Concat("Endpoint: ", source));
                    await sw.WriteLineAsync("___________________________________________________________________________________________________");
                    await sw.WriteLineAsync(string.Concat("PayLoad: ", payLoad));
                    await sw.WriteLineAsync("___________________________________________________________________________________________________");
                    await sw.WriteLineAsync(exx.ToString());
                }
            }
        }
        private Entities _db = new Entities();
        ///<summary>
        ///Post Transaction (Single)
        ///</summary>
        ///<param name="model">The ID of the data.</param>
        ///<returns>HTTP:200 for success else failure</returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostTransaction(transaction_information_uploads_model model)
        {
            try
            {
                var batch_number = string.Concat(User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"));
                model.batch_number = batch_number;
                model.uploader = User.Identity.Name;
                model.uploaded = DateTime.Now;
                //using (Entities _db = new Entities())
                {
                    //Initialize the mapper
                    var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<transaction_information_uploads_model, transaction_information_upload>()
                        );
                    //Using automapper
                    var mapper = new Mapper(config);
                    var mapped = mapper.Map<transaction_information_upload>(model);
                    Convert.ToInt32("aas");
                    if (await _db.transaction_information_upload.AnyAsync(x => x.t_account_number == model.t_account_number && x.t_trans_number == model.t_trans_number && x.t_amount_local == model.t_amount_local))
                    {
                        return BadRequest("Duplicate Transactions");
                    }
                    /*
                    _db.transaction_information_upload.Add(mapped);
                    _db.Database.CommandTimeout = 0;
                    await _db.SaveChangesAsync();
                    */

                    using (var connection = dapper.Connection)
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                            connection.Open();
                        var products = await connection.ExecuteAsync("INSERT INTO [dbo].[transaction_information_upload]           ([t_account_number]           ,[t_trans_number]           ,[t_location]           ,[transaction_description]           ,[t_date]           ,[t_teller]           ,[t_authorized]           ,[t_late_deposit]           ,[t_date_posting]           ,[t_value_date]           ,[t_transmode_code]           ,[t_amount_local]           ,[t_source_client_type]           ,[t_source_type]           ,[t_source_funds_code]           ,[t_source_currency_code]           ,[t_source_foreign_amount]           ,[t_source_exchange_rate]           ,[t_source_country]           ,[t_source_institution_code]           ,[t_source_institution_name]           ,[t_source_account_number]           ,[t_source_account_name]           ,[t_source_person_first_name]           ,[t_source_person_last_name]           ,[t_source_entity_name]           ,[t_dest_client_type]           ,[t_dest_type]           ,[t_dest_funds_code]           ,[t_dest_currency_code]           ,[t_dest_foreign_amount]           ,[t_dest_exchange_rate]           ,[t_dest_country]           ,[t_dest_institution_code]           ,[t_dest_institution_name]           ,[t_dest_account_number]           ,[t_dest_account_name]           ,[t_dest_person_first_name]           ,[t_dest_person_last_name]           ,[t_dest_entity_name]           ,[tran_type]           ,[uploader]           ,[uploaded]           ,[batch_number])     VALUES           (@t_account_number           ,@t_trans_number           ,@t_location           ,@transaction_description           ,@t_date           ,@t_teller           ,@t_authorized           ,@t_late_deposit           ,@t_date_posting           ,@t_value_date           ,@t_transmode_code           ,@t_amount_local           ,@t_source_client_type           ,@t_source_type           ,@t_source_funds_code           ,@t_source_currency_code           ,@t_source_foreign_amount           ,@t_source_exchange_rate           ,@t_source_country           ,@t_source_institution_code           ,@t_source_institution_name           ,@t_source_account_number           ,@t_source_account_name           ,@t_source_person_first_name           ,@t_source_person_last_name           ,@t_source_entity_name           ,@t_dest_client_type           ,@t_dest_type           ,@t_dest_funds_code           ,@t_dest_currency_code           ,@t_dest_foreign_amount           ,@t_dest_exchange_rate           ,@t_dest_country           ,@t_dest_institution_code           ,@t_dest_institution_name           ,@t_dest_account_number           ,@t_dest_account_name           ,@t_dest_person_first_name           ,@t_dest_person_last_name           ,@t_dest_entity_name           ,@tran_type           ,@uploader           ,@uploaded           ,@batch_number)", mapped);
                        return Json(new
                        {
                            StatusCode = "00",
                            StatusDescription = "Posted Successfully"
                        });
                    }

                    //await WriteLog(new Exception("SUCCESS"), "PostTransaction", Newtonsoft.Json.JsonConvert.SerializeObject(model), "success");
                    return Json(new
                    {
                        StatusCode = "00",
                        StatusDescription = "Posted Successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                await WriteLog(ex, "PostTransaction", Newtonsoft.Json.JsonConvert.SerializeObject(model), "error_ex");
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Post Transaction (Bulk)
        /// </summary>
        /// <param name="model">Request Body</param>
        /// <returns>HTTP:200 for success else failure</returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostTransactionBulk(List<transaction_information_uploads_model> model)
        {
            try
            {
                var batch_number = string.Concat(User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"));
                //using (Entities _db = new Entities())
                {
                    var dups = new List<DuplicateTranModel>();
                    foreach (var tran in model)
                    {
                        tran.batch_number = batch_number;
                        tran.uploader = User.Identity.Name;
                        tran.uploaded = DateTime.Now;
                        if (await _db.transaction_information_upload.AnyAsync(x => x.t_account_number == tran.t_account_number && x.t_trans_number == tran.t_trans_number && x.t_amount_local == tran.t_amount_local))
                        {
                            dups.Add(new DuplicateTranModel
                            {
                                t_account_number = tran.t_account_number,
                                t_amount_local = tran.t_amount_local,
                                t_date = tran.t_date,
                                t_trans_number = tran.t_trans_number
                            });
                        }
                    }
                   
                    if(dups.Count>0)
                    {
                        return BadRequest("Reason: Duplicate transactions," +
                            "Transactions: "+ Newtonsoft.Json.JsonConvert.SerializeObject(dups) +"");
                    }
                    //Initialize the mapper
                    var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<transaction_information_uploads_model, transaction_information_upload>()
                        );
                    //Using automapper
                    var mapper = new Mapper(config);
                    var mapped = mapper.Map<List<transaction_information_upload>>(model);

                    using (var connection = dapper.Connection)
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                            connection.Open();
                        var products = await connection.ExecuteAsync("INSERT INTO [dbo].[transaction_information_upload]           ([t_account_number]           ,[t_trans_number]           ,[t_location]           ,[transaction_description]           ,[t_date]           ,[t_teller]           ,[t_authorized]           ,[t_late_deposit]           ,[t_date_posting]           ,[t_value_date]           ,[t_transmode_code]           ,[t_amount_local]           ,[t_source_client_type]           ,[t_source_type]           ,[t_source_funds_code]           ,[t_source_currency_code]           ,[t_source_foreign_amount]           ,[t_source_exchange_rate]           ,[t_source_country]           ,[t_source_institution_code]           ,[t_source_institution_name]           ,[t_source_account_number]           ,[t_source_account_name]           ,[t_source_person_first_name]           ,[t_source_person_last_name]           ,[t_source_entity_name]           ,[t_dest_client_type]           ,[t_dest_type]           ,[t_dest_funds_code]           ,[t_dest_currency_code]           ,[t_dest_foreign_amount]           ,[t_dest_exchange_rate]           ,[t_dest_country]           ,[t_dest_institution_code]           ,[t_dest_institution_name]           ,[t_dest_account_number]           ,[t_dest_account_name]           ,[t_dest_person_first_name]           ,[t_dest_person_last_name]           ,[t_dest_entity_name]           ,[tran_type]           ,[uploader]           ,[uploaded]           ,[batch_number])     VALUES           (@t_account_number           ,@t_trans_number           ,@t_location           ,@transaction_description           ,@t_date           ,@t_teller           ,@t_authorized           ,@t_late_deposit           ,@t_date_posting           ,@t_value_date           ,@t_transmode_code           ,@t_amount_local           ,@t_source_client_type           ,@t_source_type           ,@t_source_funds_code           ,@t_source_currency_code           ,@t_source_foreign_amount           ,@t_source_exchange_rate           ,@t_source_country           ,@t_source_institution_code           ,@t_source_institution_name           ,@t_source_account_number           ,@t_source_account_name           ,@t_source_person_first_name           ,@t_source_person_last_name           ,@t_source_entity_name           ,@t_dest_client_type           ,@t_dest_type           ,@t_dest_funds_code           ,@t_dest_currency_code           ,@t_dest_foreign_amount           ,@t_dest_exchange_rate           ,@t_dest_country           ,@t_dest_institution_code           ,@t_dest_institution_name           ,@t_dest_account_number           ,@t_dest_account_name           ,@t_dest_person_first_name           ,@t_dest_person_last_name           ,@t_dest_entity_name           ,@tran_type           ,@uploader           ,@uploaded           ,@batch_number)", mapped);
                        return Json(new
                        {
                            StatusCode = "00",
                            StatusDescription = "Posted Successfully"
                        });
                    }

                    /*_db.transaction_information_upload.AddRange(mapped);
                    _db.Database.CommandTimeout = 0;
                    await _db.SaveChangesAsync();*/
                    //await WriteLog(new Exception("SUCCESS"), "PostTransactionBulk", Newtonsoft.Json.JsonConvert.SerializeObject(model), "success");
                    return Json(new
                    {
                        StatusCode = "00",
                        StatusDescription = "Posted Successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                await WriteLog(ex, "PostTransactionBulk", Newtonsoft.Json.JsonConvert.SerializeObject(model), "error_ex");
                return InternalServerError(ex);
            }

        }

        
        /// <summary>
        /// Post Account (Single)
        /// </summary>
        /// <param name="model">Request Body</param>
        /// <returns>HTTP:200 for success else failure</returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostAccount([FromBody] account_information_uploads_model model)
        {
            try
            {
                var batch_number = string.Concat(User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"));
                model.batch_number = batch_number;
                model.uploader = User.Identity.Name;
                model.uploaded = DateTime.Now;
                //using (Entities _db = new Entities())
                {
                    //Initialize the mapper
                    var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<account_information_uploads_model, account_information_upload>()
                        );
                    var config_sig = new MapperConfiguration(cfg =>
                            cfg.CreateMap<account_signatories_directors_uploads_model, account_signatories_directors_upload>()
                        );
                    //Using automapper
                    var mapper = new Mapper(config);
                    var mapper_sig = new Mapper(config_sig);
                    var signatories_model = model.signatories;

                    var mapped = mapper.Map<account_information_upload>(model);
                    var mapped_sig = mapper_sig.Map<List<account_signatories_directors_upload>>(signatories_model);
                    if (await _db.account_information_upload.AnyAsync(x => x.account_number == model.account_number))
                    {
                        return BadRequest("Duplicate Account");
                    }
                    //"INSERT INTO [dbo].[account_information_upload]           ([account_number]           ,[account_branch]           ,[account_name]           ,[client_number]           ,[account_currency_code]           ,[account_type]           ,[account_opened_date]           ,[account_closed_date]           ,[account_status_code]           ,[account_beneficiary]           ,[account_balance]           ,[entity_name]           ,[entity_incorporation_number]           ,[entity_business]           ,[entity_phone_contact_type]           ,[entity_phone_type]           ,[entity_phone_number]           ,[entity_address_type]           ,[entity_address]           ,[entity_city]           ,[entity_state]           ,[entity_country_code]           ,[entity_email]           ,[uploader]           ,[uploaded]           ,[batch_number])     VALUES           (@account_number,            ,@account_branch,            ,@account_name,            ,@client_number,            ,@account_currency_code,            ,@account_type,            ,@account_opened_date,            ,@account_closed_date,            ,@account_status_code,            ,@account_beneficiary,            ,@account_balance,            ,@entity_name,            ,@entity_incorporation_number,            ,@entity_business,            ,@entity_phone_contact_type,            ,@entity_phone_type,            ,@entity_phone_number,            ,@entity_address_type,            ,@entity_address,            ,@entity_city,            ,@entity_state,            ,@entity_country_code,            ,@entity_email,            ,@uploader,            ,@uploaded,            ,@batch_number)"

                    using (var connection = dapper.Connection)
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                            connection.Open();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                var accounts = await connection.ExecuteAsync("INSERT INTO [dbo].[account_information_upload]           ([account_number]           ,[account_branch]           ,[account_name]           ,[client_number]           ,[account_currency_code]           ,[account_type]           ,[account_opened_date]           ,[account_closed_date]           ,[account_status_code]           ,[account_beneficiary]           ,[account_balance]           ,[entity_name]           ,[entity_incorporation_number]           ,[entity_business]           ,[entity_phone_contact_type]           ,[entity_phone_type]           ,[entity_phone_number]           ,[entity_address_type]           ,[entity_address]           ,[entity_city]           ,[entity_state]           ,[entity_country_code]           ,[entity_email]           ,[uploader]           ,[uploaded]           ,[batch_number])     VALUES           (@account_number,            @account_branch,            @account_name,            @client_number,            @account_currency_code,            @account_type,            @account_opened_date,            @account_closed_date,            @account_status_code,            @account_beneficiary,            @account_balance,            @entity_name,            @entity_incorporation_number,            @entity_business,            @entity_phone_contact_type,            @entity_phone_type,            @entity_phone_number,            @entity_address_type,            @entity_address,            @entity_city,            @entity_state,            @entity_country_code,            @entity_email,            @uploader,            @uploaded,            @batch_number)", mapped, transaction);


                                var signatories = await connection.ExecuteAsync("INSERT INTO [dbo].[account_signatories_directors_upload]	           ([account_number]	           ,[client_number]	           ,[person_type]	           ,[is_primary]	           ,[person_title]	           ,[person_first_name]	           ,[person_last_name]	           ,[person_gender]	           ,[person_birth_date]	           ,[person_passport_number]	           ,[person_passport_country]	           ,[person_id_type]	           ,[person_id_number]	           ,[person_issue_country]	           ,[person_nationality]	           ,[person_tph_contact_type]	           ,[person_tph_comm_type]	           ,[person_phone_number]	           ,[person_address_type]	           ,[person_address]	           ,[person_city]	           ,[person_state]	           ,[person_country_code]	           ,[person_email]	           ,[person_occupation]	           ,[person_tax_number]	           ,[uploader]	           ,[uploaded]	           ,[batch_number])	     VALUES	           (@account_number	           ,@client_number	           ,@person_type	           ,@is_primary	           ,@person_title	           ,@person_first_name	           ,@person_last_name	           ,@person_gender	           ,@person_birth_date	           ,@person_passport_number	           ,@person_passport_country	           ,@person_id_type	           ,@person_id_number	           ,@person_issue_country	           ,@person_nationality	           ,@person_tph_contact_type	           ,@person_tph_comm_type	           ,@person_phone_number	           ,@person_address_type	           ,@person_address	           ,@person_city	           ,@person_state	           ,@person_country_code	           ,@person_email	           ,@person_occupation	           ,@person_tax_number	           ,@uploader	           ,@uploaded	           ,@batch_number)", mapped_sig, transaction);

                                transaction.Commit();
                                return Json(new
                                {
                                    StatusCode = "00",
                                    StatusDescription = "Posted Successfully"
                                });
                            }
                            catch (Exception iex)
                            {

                                transaction.Rollback();
                                return InternalServerError();
                            }



                        }
                    }

                    /*_db.account_information_upload.Add(mapped);
                    _db.account_signatories_directors_upload.AddRange(mapped_sig);
                    _db.Database.CommandTimeout = 0;
                    await _db.SaveChangesAsync();
                    //await WriteLog(new Exception("SUCCESS"), "PostAccount", Newtonsoft.Json.JsonConvert.SerializeObject(model), "success");
                    return Json(new
                    {
                        StatusCode = "00",
                        StatusDescription = "Posted Successfully"
                    });*/
                }
            }
            catch (Exception ex)
            {
                await WriteLog(ex, "PostAccount", Newtonsoft.Json.JsonConvert.SerializeObject(model), "error_ex");
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Post Account (Bulk)
        /// </summary>
        /// <param name="model">Request Body</param>
        /// <returns>HTTP:200 for success else failure</returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostAccountBulk(List<account_information_uploads_model> model)
        {
            try
            {
                var batch_number = string.Concat(User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"));
                foreach (var acct in model)
                {
                    acct.batch_number = batch_number;
                    acct.uploader = User.Identity.Name;
                    acct.uploaded = DateTime.Now;
                }

                //using (Entities _db = new Entities())
                {
                    var dups = new List<DuplicateAccountModel>();
                    foreach (var acct in model)
                    {
                        acct.batch_number = batch_number;
                        acct.uploader = User.Identity.Name;
                        acct.uploaded = DateTime.Now;
                        if (await _db.account_information_upload.AnyAsync(x => x.account_number == acct.account_number))
                        {
                            dups.Add(new DuplicateAccountModel
                            {
                                account_number = acct.account_number,
                                account_name = acct.account_name,

                            });
                        }
                    }
                    if (dups.Count > 0)
                    {
                        return BadRequest("Reason: Duplicate Account," +
                            "Accounts: " + Newtonsoft.Json.JsonConvert.SerializeObject(dups) + "");
                    }

                    //Initialize the mapper
                    var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<List<account_information_uploads_model>, List<account_information_upload>>()
                        );
                    var config_sig = new MapperConfiguration(cfg =>
                            cfg.CreateMap<account_signatories_directors_uploads_model, account_signatories_directors_upload>()
                        );
                    //Using automapper
                    var mapper = new Mapper(config);
                    var mapper_sig = new Mapper(config_sig);
                    var signatories_model = new List<account_signatories_directors_uploads_model>();
                    model.ForEach(x => signatories_model.AddRange(x.signatories));

                    var mapped = mapper.Map<List<account_information_upload>>(model);
                    var mapped_sig = mapper_sig.Map<List<account_signatories_directors_upload>>(signatories_model);

                    using (var connection = dapper.Connection)
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                            connection.Open();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                var accounts = await connection.ExecuteAsync("INSERT INTO [dbo].[account_information_upload]           ([account_number]           ,[account_branch]           ,[account_name]           ,[client_number]           ,[account_currency_code]           ,[account_type]           ,[account_opened_date]           ,[account_closed_date]           ,[account_status_code]           ,[account_beneficiary]           ,[account_balance]           ,[entity_name]           ,[entity_incorporation_number]           ,[entity_business]           ,[entity_phone_contact_type]           ,[entity_phone_type]           ,[entity_phone_number]           ,[entity_address_type]           ,[entity_address]           ,[entity_city]           ,[entity_state]           ,[entity_country_code]           ,[entity_email]           ,[uploader]           ,[uploaded]           ,[batch_number])     VALUES           (@account_number,            @account_branch,            @account_name,            @client_number,            @account_currency_code,            @account_type,            @account_opened_date,            @account_closed_date,            @account_status_code,            @account_beneficiary,            @account_balance,            @entity_name,            @entity_incorporation_number,            @entity_business,            @entity_phone_contact_type,            @entity_phone_type,            @entity_phone_number,            @entity_address_type,            @entity_address,            @entity_city,            @entity_state,            @entity_country_code,            @entity_email,            @uploader,            @uploaded,            @batch_number)", mapped, transaction);


                                var signatories = await connection.ExecuteAsync("INSERT INTO [dbo].[account_signatories_directors_upload]	           ([account_number]	           ,[client_number]	           ,[person_type]	           ,[is_primary]	           ,[person_title]	           ,[person_first_name]	           ,[person_last_name]	           ,[person_gender]	           ,[person_birth_date]	           ,[person_passport_number]	           ,[person_passport_country]	           ,[person_id_type]	           ,[person_id_number]	           ,[person_issue_country]	           ,[person_nationality]	           ,[person_tph_contact_type]	           ,[person_tph_comm_type]	           ,[person_phone_number]	           ,[person_address_type]	           ,[person_address]	           ,[person_city]	           ,[person_state]	           ,[person_country_code]	           ,[person_email]	           ,[person_occupation]	           ,[person_tax_number]	           ,[uploader]	           ,[uploaded]	           ,[batch_number])	     VALUES	           (@account_number	           ,@client_number	           ,@person_type	           ,@is_primary	           ,@person_title	           ,@person_first_name	           ,@person_last_name	           ,@person_gender	           ,@person_birth_date	           ,@person_passport_number	           ,@person_passport_country	           ,@person_id_type	           ,@person_id_number	           ,@person_issue_country	           ,@person_nationality	           ,@person_tph_contact_type	           ,@person_tph_comm_type	           ,@person_phone_number	           ,@person_address_type	           ,@person_address	           ,@person_city	           ,@person_state	           ,@person_country_code	           ,@person_email	           ,@person_occupation	           ,@person_tax_number	           ,@uploader	           ,@uploaded	           ,@batch_number)", mapped_sig,transaction);

                                transaction.Commit();
                                return Json(new
                                {
                                    StatusCode = "00",
                                    StatusDescription = "Posted Successfully"
                                });
                            }
                            catch (Exception)
                            {

                                transaction.Rollback();
                                return InternalServerError();
                            }



                        }
                    }

                    /*_db.account_information_upload.AddRange(mapped);
                    _db.account_signatories_directors_upload.AddRange(mapped_sig);
                    _db.Database.CommandTimeout = 0;
                    await _db.SaveChangesAsync();
                    //await WriteLog(new Exception("SUCCESS"), "PostAccountBulk", Newtonsoft.Json.JsonConvert.SerializeObject(model), "success");
                    return Json(new
                    {
                        StatusCode = "00",
                        StatusDescription = "Posted Successfully"
                    });*/
                }
            }
            catch (Exception ex)
            {
                await WriteLog(ex, "PostAccountBulk", Newtonsoft.Json.JsonConvert.SerializeObject(model), "error_ex");
                return InternalServerError(ex);
            }
        }

        
        /// <summary>
        /// Post Signatory (Single)
        /// </summary>
        /// <param name="model">Request Body</param>
        /// <returns>HTTP:200 for success else failure</returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostSignatory(account_signatories_directors_uploads_model model)
        {
            try
            {

                var batch_number = string.Concat(User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"));
                model.batch_number = batch_number;
                model.uploader = User.Identity.Name;
                model.uploaded = DateTime.Now;
                //using (Entities _db = new Entities())
                {
                    //Initialize the mapper
                    var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<account_signatories_directors_uploads_model, account_signatories_directors_upload>()
                        );
                    //Using automapper
                    var mapper = new Mapper(config);
                    var mapped = mapper.Map<account_signatories_directors_upload>(model);
                    if (await _db.account_signatories_directors_upload.AnyAsync(x => x.account_number == model.account_number && x.person_first_name == model.person_first_name && x.person_last_name == model.person_last_name && x.person_gender == model.person_gender && x.person_birth_date == model.person_birth_date))
                    {
                        return BadRequest("Duplicate Signatory");
                    }


                    using (var connection = dapper.Connection)
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                            connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                


                                var signatories = await connection.ExecuteAsync("INSERT INTO [dbo].[account_signatories_directors_upload]	           ([account_number]	           ,[client_number]	           ,[person_type]	           ,[is_primary]	           ,[person_title]	           ,[person_first_name]	           ,[person_last_name]	           ,[person_gender]	           ,[person_birth_date]	           ,[person_passport_number]	           ,[person_passport_country]	           ,[person_id_type]	           ,[person_id_number]	           ,[person_issue_country]	           ,[person_nationality]	           ,[person_tph_contact_type]	           ,[person_tph_comm_type]	           ,[person_phone_number]	           ,[person_address_type]	           ,[person_address]	           ,[person_city]	           ,[person_state]	           ,[person_country_code]	           ,[person_email]	           ,[person_occupation]	           ,[person_tax_number]	           ,[uploader]	           ,[uploaded]	           ,[batch_number])	     VALUES	           (@account_number	           ,@client_number	           ,@person_type	           ,@is_primary	           ,@person_title	           ,@person_first_name	           ,@person_last_name	           ,@person_gender	           ,@person_birth_date	           ,@person_passport_number	           ,@person_passport_country	           ,@person_id_type	           ,@person_id_number	           ,@person_issue_country	           ,@person_nationality	           ,@person_tph_contact_type	           ,@person_tph_comm_type	           ,@person_phone_number	           ,@person_address_type	           ,@person_address	           ,@person_city	           ,@person_state	           ,@person_country_code	           ,@person_email	           ,@person_occupation	           ,@person_tax_number	           ,@uploader	           ,@uploaded	           ,@batch_number)", mapped, transaction);

                                transaction.Commit();
                                return Json(new
                                {
                                    StatusCode = "00",
                                    StatusDescription = "Posted Successfully"
                                });
                            }
                            catch (Exception)
                            {

                                transaction.Rollback();
                                return InternalServerError();
                            }



                        }
                    }


                    /*_db.account_signatories_directors_upload.Add(mapped);
                    _db.Database.CommandTimeout = 0;
                    await _db.SaveChangesAsync();
                    //await WriteLog(new Exception("SUCCESS"), "PostSignatory", Newtonsoft.Json.JsonConvert.SerializeObject(model), "success");
                    return Json(new
                    {
                        StatusCode = "00",
                        StatusDescription = "Posted Successfully"
                    });*/
                }
            }
            catch (Exception ex)
            {
                await WriteLog(ex, "PostSignatory", Newtonsoft.Json.JsonConvert.SerializeObject(model), "error_ex");
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Post Signatory (Bulk)
        /// </summary>
        /// <param name="model">Request Body</param>
        /// <returns>HTTP:200 for success else failure</returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostSignatoryBulk(List<account_signatories_directors_uploads_model> model)
        {
            try
            {
                var batch_number = string.Concat(User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"));
                foreach (var sig in model)
                {
                    sig.batch_number = batch_number;
                    sig.uploader = User.Identity.Name;
                    sig.uploaded = DateTime.Now;
                }
                //using (Entities _db = new Entities())
                {
                    var dups = new List<DuplicateSignatoryModel>();
                    var uploaded = DateTime.Now;
                    foreach (var sig in model)
                    {
                        sig.batch_number = batch_number;
                        sig.uploader = User.Identity.Name;
                        sig.uploaded = uploaded;
                        if (await _db.account_signatories_directors_upload.AnyAsync(x => x.account_number == sig.account_number && x.person_first_name == sig.person_first_name && x.person_last_name == sig.person_last_name && x.person_gender == sig.person_gender && x.person_birth_date == sig.person_birth_date))
                        {
                            dups.Add(new DuplicateSignatoryModel
                            {
                                account_number = sig.account_number,
                                person_first_name = sig.person_first_name,
                                person_last_name = sig.person_last_name,
                                person_gender = sig.person_gender,
                                person_birth_date = sig.person_birth_date,

                            });
                        }
                    }
                    if (dups.Count > 0)
                    {
                        return BadRequest("Reason: Duplicate Signatories," +
                            "Signatory: " + Newtonsoft.Json.JsonConvert.SerializeObject(dups) + "");
                    }


                    //Initialize the mapper
                    var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<account_signatories_directors_uploads_model, account_signatories_directors_upload>()
                        );
                    //Using automapper
                    var mapper = new Mapper(config);
                    var mapped = mapper.Map<List<account_signatories_directors_upload>>(model);

                    using (var connection = dapper.Connection)
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                            connection.Open();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                               


                                var signatories = await connection.ExecuteAsync("INSERT INTO [dbo].[account_signatories_directors_upload]	           ([account_number]	           ,[client_number]	           ,[person_type]	           ,[is_primary]	           ,[person_title]	           ,[person_first_name]	           ,[person_last_name]	           ,[person_gender]	           ,[person_birth_date]	           ,[person_passport_number]	           ,[person_passport_country]	           ,[person_id_type]	           ,[person_id_number]	           ,[person_issue_country]	           ,[person_nationality]	           ,[person_tph_contact_type]	           ,[person_tph_comm_type]	           ,[person_phone_number]	           ,[person_address_type]	           ,[person_address]	           ,[person_city]	           ,[person_state]	           ,[person_country_code]	           ,[person_email]	           ,[person_occupation]	           ,[person_tax_number]	           ,[uploader]	           ,[uploaded]	           ,[batch_number])	     VALUES	           (@account_number	           ,@client_number	           ,@person_type	           ,@is_primary	           ,@person_title	           ,@person_first_name	           ,@person_last_name	           ,@person_gender	           ,@person_birth_date	           ,@person_passport_number	           ,@person_passport_country	           ,@person_id_type	           ,@person_id_number	           ,@person_issue_country	           ,@person_nationality	           ,@person_tph_contact_type	           ,@person_tph_comm_type	           ,@person_phone_number	           ,@person_address_type	           ,@person_address	           ,@person_city	           ,@person_state	           ,@person_country_code	           ,@person_email	           ,@person_occupation	           ,@person_tax_number	           ,@uploader	           ,@uploaded	           ,@batch_number)", mapped,transaction);

                                transaction.Commit();
                                return Json(new
                                {
                                    StatusCode = "00",
                                    StatusDescription = "Posted Successfully"
                                });
                            }
                            catch (Exception)
                            {

                                transaction.Rollback();
                                return InternalServerError();
                            }



                        }
                    }

                    /*_db.account_signatories_directors_upload.AddRange(mapped);
                    _db.Database.CommandTimeout = 0;
                    await _db.SaveChangesAsync();
                    //await WriteLog(new Exception("SUCCESS"), "PostSignatoryBulk", Newtonsoft.Json.JsonConvert.SerializeObject(model), "success");
                    return Json(new
                    {
                        StatusCode = "00",
                        StatusDescription = "Posted Successfully"
                    });*/
                }
            }
            catch (Exception ex)
            {
                await WriteLog(ex, "PostSignatoryBulk", Newtonsoft.Json.JsonConvert.SerializeObject(model), "error_ex");
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
