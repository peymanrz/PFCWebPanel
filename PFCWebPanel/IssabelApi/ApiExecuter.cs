using PFCWebPanel.QueueApiClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using PersianFiberWeb.ExtensionList;


namespace PFCWebPanel
{
    public class ApiExecuter
    {
        string IssabeLIP = PFCWebPanel.Properties.Settings.Default.IssabeLIP;
        string IssabeLUser = PFCWebPanel.Properties.Settings.Default.IssabeLUser;
        string IssabeLPassword = PFCWebPanel.Properties.Settings.Default.IssabeLPassword;
        //string LinuXUser = PFCWebPanel.Properties.Settings.Default.LinuXUser;
        //string LinuXPassword = PFCWebPanel.Properties.Settings.Default.LinuXPassword;



        public async Task<List<ExtensionS>> GetExtensionListRestApi()
        {

            retry:
            string token = await GetNewTokenRest();
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(delegate { return true; });
            HttpClient client = new HttpClient();
            string postadd = "https://" + IssabeLIP + "/pbxapi/extensions";
            var request = new HttpRequestMessage(HttpMethod.Get, postadd);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(request);
            string res = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                if (res.Contains("secret"))
                {
                    //res = res.Substring(31);
                    //res = res.Substring(0, (res.Length - 2));

                    //return res;
                    List<ExtensionS> extensionlist = GettingExtensionList.FromJson(res).Results;
                    return extensionlist;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<QueueApiViewModel> GetQueueRestApi(long queueNumber)
        {
            retry:
            string token = await GetNewTokenRest();
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            HttpClient client = new HttpClient();
            string postadd = "https://" + IssabeLIP + "/pbxapi/queues/" + queueNumber.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, postadd);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(request);
            string res = await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                if (res.Contains("password"))
                {
                    res = res.Substring(31);
                    res = res.Substring(0, (res.Length - 2));

                    //return res;
                    QueueApi queue = QueueApi.FromJson(res);
                    QueueApiViewModel queuemodel = new QueueApiViewModel();

                    queuemodel.Extension = Convert.ToInt32(queue.Extension);
                    queuemodel.Name = queue.Name;
                    queuemodel.Password = queue.Password;
                    //queuemodel.StaticMembers.Add("2000");// = queue.StaticMembers;
                    queuemodel.RingStrategy = queue.RingStrategy;
                    queuemodel.SkipBusyAgents = queue.SkipBusyAgents;
                    queuemodel.JoinAnnounceId = queue.JoinAnnounceId;
                    queuemodel.MonitorFormat = queue.MonitorFormat;
                    //queuemodel.MonitorJoin = "yes";
                    queuemodel.MonitorType = queue.MonitorType;
                    queuemodel.MaxWait = queue.MaxWait;
                    // queuemodel.SoundThankYou = "queue-thankyou";
                    //queuemodel.SoundCallsWaiting = "queue-callswaiting";
                    //queuemodel.SoundThereAre = "queue-thereare";
                    // queuemodel.SoundYouAreNext = "queue-youarenext";
                    queuemodel.TimeoutPriority = queue.TimeoutPriority;         /////Max Wait Time Mode
                    queuemodel.Timeout = queue.Timeout;           /////////// Agent Timeout
                    queuemodel.AgentRetry = queue.AgentRetry;
                    queuemodel.ReportHoldTime = queue.ReportHoldTime;
                    queuemodel.AutoPause = queue.AutoPause;
                    queuemodel.AutoPauseDelay = Convert.ToInt32(queue.AutoPauseDelay);
                    queuemodel.AutoPauseIfBusy = queue.AutoPauseIfBusy;
                    queuemodel.AutoPauseIfUnavailable = queue.AutoPauseIfUnavailable;
                    queuemodel.MaxCallersWaiting = queue.MaxCallersWaiting;
                    queuemodel.JoinEmpty = queue.JoinEmpty;
                    queuemodel.LeaveWhenEmpty = queue.LeaveWhenEmpty;
                    queuemodel.AnnounceFrequency = queue.AnnounceFrequency;
                    queuemodel.AnnounceHoldtime = queue.AnnounceHoldtime;
                    queuemodel.AnnouncePosition = queue.AnnouncePosition;
                    //queuemodel.EventMemberStatus = "yes";
                    //queuemodel.EventWhenCalled = "yes";
                    queuemodel.CronSchedule = queue.CronSchedule;


                    return queuemodel;
                }
                else
                {
                    return null;
                }
            }
            else if (response.StatusCode.ToString() == "500")
            {
                //GetNewToken();
                goto retry;
            }
            else
            {
                return null;
            }


            //ExtensionApiViewModel extensionViewModel = new ExtensionApiViewModel();
            //extensionViewModel.Extension = extension.Extension.Value;
            //extensionViewModel.Name = extension.Name;
            //extensionViewModel.Secret = extension.Secret;
            //extensionViewModel.DeviceOptions_DtmfMode = extension.DeviceOptions.DtmfMode;
            //extensionViewModel.DeviceOptions_Encryption = extension.DeviceOptions.Encryption;
            //extensionViewModel.DeviceOptions_Nat = extension.DeviceOptions.Nat;
            //extensionViewModel.DeviceOptions_Port = extension.DeviceOptions.Port.Value;
            //extensionViewModel.DeviceOptions_Transport = extension.DeviceOptions.Transport;
            //extensionViewModel.Recording_InboundExternal = extension.Recording.InboundExternal;
            //extensionViewModel.Recording_InboundInternal = extension.Recording.InboundInternal;
            //extensionViewModel.Recording_OutboundExternal = extension.Recording.OutboundExternal;
            //extensionViewModel.Recording_OutboundInternal = extension.Recording.OutboundInternal;
            //extensionViewModel.Recording_Priority = extension.Recording.Priority.Value;

            //extensionViewModel.Voicemail_Voicemail = "selected";
            //extensionViewModel.Voicemail_Password = 2222;
            //extensionViewModel.Voicemail_EmailAddress = "aa@bb.net";
            //extensionViewModel.Voicemail_EmailAttachmen = "yes";
            //extensionViewModel.Voicemail_PlayCID = "yes";
            //extensionViewModel.Voicemail_PlayEnvelope = "yes";
            //extensionViewModel.Voicemail_DeleteVoicemail = "yes";



            //return extensionViewModel;


        }
        public async Task<string> EditQueueOperatorsRestApi(string queueNumber, IEnumerable<string> extensionList)
        {
            List<string> StMembers = new List<string>();
            if (extensionList != null)
            {
                foreach (var item in extensionList)
                {
                    StMembers.Add(item + ",0");
                }
            }

            string token = await GetNewTokenRest();
            QueueApi queueApi = new QueueApi();
            queueApi.Extension = queueNumber.ToString();
            queueApi.StaticMembers = StMembers;
            string json = queueApi.ToJson();
            int Failretry = 0;
            retry:
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            HttpClient client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string Postadd = "https://" + IssabeLIP + "/pbxapi/queues/" + queueNumber.ToString();
            var response = await client.PutAsync(Postadd, content);
            string res; //= response.ToString();
            if (response.IsSuccessStatusCode)
            {
                res = "true";
            }
            else // if (res.Contains("StatusCode: 500") || response.StatusCode.ToString() == "500")
            {
                Failretry += 1;
                if (Failretry < 10)
                {

                    goto retry;
                }
                else
                {
                    res = "ثبت با خطا روبرو شد با مدیر سیستم تماس بگیرید";
                }
            }
            return res;
        }
        //public async Task<string> NewQueueRestApi(QueueApiViewModel queueApiViewModel)
        //{
        //    //List<string> stmembers = new List<string>();
        //    //stmembers.Add("1000,0");
        //    //stmembers.Add("3580,0");
        //    string token = await GetNewTokenRest();
        //    QueueApi queueApi = new QueueApi();
        //    queueApi.Extension = queueApiViewModel.Extension.ToString();
        //    queueApi.Name = queueApiViewModel.Name;
        //    queueApi.Password = queueApiViewModel.Password.ToString();
        //    //queueApi.StaticMembers = null;
        //    // = queueApiViewModel.StaticMembers;
        //    queueApi.RingStrategy = queueApiViewModel.RingStrategy;
        //    queueApi.SkipBusyAgents = queueApiViewModel.SkipBusyAgents.ToString();
        //    queueApi.JoinAnnounceId = queueApiViewModel.JoinAnnounceId.ToString();
        //    queueApi.MonitorFormat = queueApiViewModel.MonitorFormat;
        //    queueApi.MonitorJoin = "yes";
        //    queueApi.MonitorType = queueApiViewModel.MonitorType;
        //    queueApi.MaxWait = queueApiViewModel.MaxWait;
        //    queueApi.SoundThankYou = "queue-thankyou";
        //    queueApi.SoundCallsWaiting = "queue-callswaiting";
        //    queueApi.SoundThereAre = "queue-thereare";
        //    queueApi.SoundYouAreNext = "queue-youarenext";
        //    queueApi.TimeoutPriority = queueApiViewModel.TimeoutPriority;         /////Max Wait Time Mode
        //    queueApi.Timeout = queueApiViewModel.Timeout.ToString();           /////////// Agent Timeout
        //    queueApi.AgentRetry = queueApiViewModel.AgentRetry;
        //    queueApi.ReportHoldTime = queueApiViewModel.ReportHoldTime;
        //    queueApi.AutoPause = queueApiViewModel.AutoPause;
        //    queueApi.AutoPauseDelay = queueApiViewModel.AutoPauseDelay.ToString();
        //    queueApi.AutoPauseIfBusy = queueApiViewModel.AutoPauseIfBusy;
        //    queueApi.AutoPauseIfUnavailable = queueApiViewModel.AutoPauseIfUnavailable;
        //    queueApi.MaxCallersWaiting = queueApiViewModel.MaxCallersWaiting.ToString();
        //    queueApi.JoinEmpty = queueApiViewModel.JoinEmpty;
        //    queueApi.LeaveWhenEmpty = queueApiViewModel.LeaveWhenEmpty;
        //    queueApi.AnnounceFrequency = queueApiViewModel.AnnounceFrequency.ToString();
        //    queueApi.AnnounceHoldtime = queueApiViewModel.AnnounceHoldtime;
        //    queueApi.AnnouncePosition = queueApiViewModel.AnnouncePosition;
        //    queueApi.EventMemberStatus = "yes";
        //    queueApi.EventWhenCalled = "yes";
        //    queueApi.CronSchedule = queueApiViewModel.CronSchedule;


        //    string json = queueApi.ToJson();
        //    int Sucretry = 0;
        //    int Failretry = 0;
        //    retry:
        //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
        //    HttpClient client = new HttpClient();
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    string Postadd = "https://" + IssabeLIP + "/pbxapi/queues/" + queueApiViewModel.Extension.ToString();
        //    var response = await client.PutAsync(Postadd, content);
        //    string res; //= response.ToString();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        Sucretry += 1;
        //        if (Sucretry < 2)
        //        {
        //            goto retry;
        //        }
        //        else
        //        {
        //            // res = await response.Content.ReadAsStringAsync();
        //            res = "true";
        //        }

        //    }
        //    else // if (res.Contains("StatusCode: 500") || response.StatusCode.ToString() == "500")
        //    {
        //        Failretry += 1;


        //        if (Failretry < 10)
        //        {
        //            GetNewToken();
        //            goto retry;
        //        }
        //        else
        //        {

        //            res = "ثبت با خطا روبرو شد با مدیر سیستم تماس بگیرید";
        //        }
        //    }
        //    return res;
        //}
        //public async Task<string> EditQueueRestApi(QueueApiViewModel queueApiViewModel)
        //{
        //    string token = await GetNewTokenRest();
        //    var oplist = queryRepository.QueueStaticMembersList(queueApiViewModel.Extension);
        //    List<string> StMembers = new List<string>();
        //    if (oplist != null)
        //    {
        //        foreach (var item in oplist)
        //        {
        //            StMembers.Add(item + ",0");
        //        }
        //    }

        //    QueueApi queueApi = new QueueApi();
        //    queueApi.Extension = queueApiViewModel.Extension.ToString();
        //    queueApi.Name = queueApiViewModel.Name;
        //    queueApi.Password = queueApiViewModel.Password.ToString();
        //    queueApi.StaticMembers = StMembers;// = queueApiViewModel.StaticMembers;
        //    queueApi.RingStrategy = queueApiViewModel.RingStrategy;
        //    queueApi.SkipBusyAgents = queueApiViewModel.SkipBusyAgents.ToString();
        //    queueApi.JoinAnnounceId = queueApiViewModel.JoinAnnounceId.ToString();
        //    queueApi.MonitorFormat = queueApiViewModel.MonitorFormat;
        //    queueApi.MonitorJoin = "yes";
        //    queueApi.MonitorType = queueApiViewModel.MonitorType;
        //    queueApi.MaxWait = queueApiViewModel.MaxWait;
        //    queueApi.SoundThankYou = "queue-thankyou";
        //    queueApi.SoundCallsWaiting = "queue-callswaiting";
        //    queueApi.SoundThereAre = "queue-thereare";
        //    queueApi.SoundYouAreNext = "queue-youarenext";
        //    queueApi.TimeoutPriority = queueApiViewModel.TimeoutPriority;         /////Max Wait Time Mode
        //    queueApi.Timeout = queueApiViewModel.Timeout.ToString();           /////////// Agent Timeout
        //    queueApi.AgentRetry = queueApiViewModel.AgentRetry;
        //    queueApi.ReportHoldTime = queueApiViewModel.ReportHoldTime;
        //    queueApi.AutoPause = queueApiViewModel.AutoPause;
        //    queueApi.AutoPauseDelay = queueApiViewModel.AutoPauseDelay.ToString();
        //    queueApi.AutoPauseIfBusy = queueApiViewModel.AutoPauseIfBusy;
        //    queueApi.AutoPauseIfUnavailable = queueApiViewModel.AutoPauseIfUnavailable;
        //    queueApi.MaxCallersWaiting = queueApiViewModel.MaxCallersWaiting.ToString();
        //    queueApi.JoinEmpty = queueApiViewModel.JoinEmpty;
        //    queueApi.LeaveWhenEmpty = queueApiViewModel.LeaveWhenEmpty;
        //    queueApi.AnnounceFrequency = queueApiViewModel.AnnounceFrequency.ToString();
        //    queueApi.AnnounceHoldtime = queueApiViewModel.AnnounceHoldtime;
        //    queueApi.AnnouncePosition = queueApiViewModel.AnnouncePosition;
        //    //queueApi.EventMemberStatus = "yes";
        //    //queueApi.EventWhenCalled = "yes";
        //    queueApi.CronSchedule = queueApiViewModel.CronSchedule;


        //    string json = queueApi.ToJson();
        //    //int Sucretry = 0;
        //    int Failretry = 0;
        //    retry:
        //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
        //    HttpClient client = new HttpClient();
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    string Postadd = "https://" + IssabeLIP + "/pbxapi/queues/" + queueApiViewModel.Extension.ToString();
        //    var response = await client.PutAsync(Postadd, content);
        //    string res; //= response.ToString();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        //Sucretry += 1;
        //        //if (Sucretry < 2)
        //        //{
        //        //    goto retry;
        //        //}
        //        //else
        //        //{
        //        res = await response.Content.ReadAsStringAsync();
        //        res = "true";
        //        // }

        //    }
        //    else // if (res.Contains("StatusCode: 500") || response.StatusCode.ToString() == "500")
        //    {
        //        Failretry += 1;


        //        if (Failretry < 10)
        //        {
        //            GetNewToken();
        //            goto retry;
        //        }
        //        else
        //        {

        //            res = "ثبت با خطا روبرو شد با مدیر سیستم تماس بگیرید" + "--" + response.ToString() + "--" + json + "--";
        //        }
        //    }
        //    return res;
        //}

        //public async Task<string> DeleteExtensionRestApi(long extensionNumber)
        //{
        //    retry:
        //    string token = await GetNewTokenRest();
        //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
        //    HttpClient client = new HttpClient();
        //    string postadd = "https://" + IssabeLIP + "/pbxapi/extensions/" + extensionNumber.ToString();
        //    var request = new HttpRequestMessage(HttpMethod.Delete, postadd);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var response = await client.SendAsync(request);
        //    string res = "";


        //    if (response.IsSuccessStatusCode)
        //    {
        //        res = "true";
        //    }
        //    else if (response.StatusCode.ToString() == "500")
        //    {
        //        GetNewToken();
        //        goto retry;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    return res;














        //    //string token = GetNewToken(IssabeLIP, LinuXUser, LinuXPassword, IssabeLUser, IssabeLPassword);
        //    //var rawlist = (ExecuteSshCommand(IssabeLIP, LinuXUser, LinuXPassword, "curl -v -L -k -X DELETE -H  \"Authorization: Bearer " + token + "\" https://localhost/pbxapi/extensions/" + extensionNumber.ToString() + " | python -m json.tool "));


        //    //string[] res = new string[3];
        //    //if (rawlist[3].Contains("HTTP/1.1 200 OK"))
        //    //{
        //    //    res[0] = "true";
        //    //    res[1] = "";
        //    //}
        //    //else
        //    //{
        //    //    res[0] = "false";
        //    //    res[1] = "ارتباط با سرور برقرار نشد";
        //    //}

        //    //return res;

        //}
        //public async Task<string> EditExtensionRestApi(ExtensionApiViewModel extensionViewModel)
        //{
        //    //byte[] bytes = Encoding.ASCII.GetBytes(extensionViewModel.Name); ;

        //    retry: string token = await GetNewTokenRest();
        //    ExtensionApiClass extension = new ExtensionApiClass
        //    {
        //        Extension = extensionViewModel.Extension,
        //        Name = extensionViewModel.Name,
        //        Secret = extensionViewModel.Secret
        //    };
        //    DeviceOptions extensiondeviceoptions = new DeviceOptions();
        //    extensiondeviceoptions.DtmfMode = extensionViewModel.DeviceOptions_DtmfMode;
        //    extensiondeviceoptions.Encryption = extensionViewModel.DeviceOptions_Encryption;
        //    extensiondeviceoptions.Nat = extensionViewModel.DeviceOptions_Nat;
        //    extensiondeviceoptions.Port = extensionViewModel.DeviceOptions_Port;
        //    extensiondeviceoptions.Transport = extensionViewModel.DeviceOptions_Transport;
        //    Recording extensionrecording = new Recording();
        //    extensionrecording.InboundExternal = extensionViewModel.Recording_InboundExternal;
        //    extensionrecording.InboundInternal = extensionViewModel.Recording_InboundInternal;
        //    extensionrecording.OutboundExternal = extensionViewModel.Recording_OutboundExternal;
        //    extensionrecording.OutboundInternal = extensionViewModel.Recording_OutboundInternal;
        //    extensionrecording.Priority = extensionViewModel.Recording_Priority;
        //    extension.DeviceOptions = extensiondeviceoptions;
        //    extension.Recording = extensionrecording;

        //    ExtensionOptions extensionOptions = new ExtensionOptions();
        //    extensionOptions.CallForwardRingTime = extensionViewModel.cfringtimer;
        //    extensionOptions.CallWaiting = extensionViewModel.callwaiting;
        //    extensionOptions.OutboundConcurrencyLimit = extensionViewModel.concurrency_limit;
        //    extensionOptions.RingTime = extensionViewModel.ringtimer;
        //    extension.ExtensionOptions = extensionOptions;

        //    if (extensionViewModel.Voicemail_Voicemail == "yes")
        //    {

        //        Voicemail exvoicemail = new Voicemail();
        //        exvoicemail.Enabled = "yes";
        //        exvoicemail.Pin = extensionViewModel.Voicemail_Password;
        //        exvoicemail.Email = extensionViewModel.Voicemail_EmailAddress;

        //        Options exvoiceopt = new Options();
        //        exvoiceopt.EmailAttachment = extensionViewModel.Voicemail_EmailAttachmen;
        //        exvoiceopt.PlayCallerid = extensionViewModel.Voicemail_PlayCID;
        //        exvoiceopt.PlayEnvelope = extensionViewModel.Voicemail_PlayEnvelope;
        //        exvoiceopt.Delete = extensionViewModel.Voicemail_DeleteVoicemail;


        //        extension.Voicemail = exvoicemail;
        //        extension.Voicemail.Options = exvoiceopt;
        //    }
        //    else
        //    {
        //        //extension.Voicemail.Enabled = "no";

        //    }














        //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
        //    HttpClient client = new HttpClient();
        //    string json = extension.ToJson();
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    string postadd = "https://" + IssabeLIP + "/pbxapi/extensions/" + extensionViewModel.Extension.ToString();
        //    var response = await client.PutAsync(postadd, content);
        //    string res = "";
        //    if (response.IsSuccessStatusCode)
        //    {
        //        //res = await response.Content.ReadAsStringAsync();
        //        res = "true";
        //    }
        //    else if (response.StatusCode.ToString() == "422")
        //    {
        //        await DeleteExtensionRestApi(extensionViewModel.Extension);
        //        goto retry;
        //    }
        //    else if (response.StatusCode.ToString() == "500")
        //    {
        //        GetNewToken();
        //        goto retry;
        //    }
        //    else
        //    {
        //        res = response.ToString();
        //    }

        //    return res;

        //    //Voicemail_Voicemail
        //    //Voicemail_Password
        //    //Voicemail_EmailAddress
        //    //Voicemail_EmailAttachmen
        //    //Voicemail_PlayCID
        //    //Voicemail_PlayEnvelope
        //    //Voicemail_DeleteVoicemail

        //}
        //public async Task<ExtensionApiViewModel> GetExtensionRestApi(long extensionNumber)
        //{
        //    retry:
        //    string token = await GetNewTokenRest();
        //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
        //    HttpClient client = new HttpClient();
        //    string postadd = "https://" + IssabeLIP + "/pbxapi/extensions/" + extensionNumber.ToString();
        //    var request = new HttpRequestMessage(HttpMethod.Get, postadd);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var response = await client.SendAsync(request);
        //    string res = await response.Content.ReadAsStringAsync();


        //    if (response.IsSuccessStatusCode)
        //    {
        //        if (!res.Contains("secret"))
        //        {

        //        }
        //    }
        //    else if (response.StatusCode.ToString() == "500")
        //    {
        //        GetNewToken();
        //        goto retry;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    res = res.Substring(31);
        //    res = res.Substring(0, (res.Length - 2));
        //    ExtensionApiClass extension = ExtensionApiClass.FromJson(res);

        //    ExtensionApiViewModel extensionViewModel = new ExtensionApiViewModel();
        //    extensionViewModel.Extension = extension.Extension.Value;
        //    extensionViewModel.Name = extension.Name;
        //    extensionViewModel.Secret = extension.Secret;
        //    extensionViewModel.DeviceOptions_DtmfMode = extension.DeviceOptions.DtmfMode;
        //    extensionViewModel.DeviceOptions_Encryption = extension.DeviceOptions.Encryption;
        //    extensionViewModel.DeviceOptions_Nat = extension.DeviceOptions.Nat;
        //    extensionViewModel.DeviceOptions_Port = extension.DeviceOptions.Port.Value;
        //    extensionViewModel.DeviceOptions_Transport = extension.DeviceOptions.Transport;
        //    extensionViewModel.Recording_InboundExternal = extension.Recording.InboundExternal;
        //    extensionViewModel.Recording_InboundInternal = extension.Recording.InboundInternal;
        //    extensionViewModel.Recording_OutboundExternal = extension.Recording.OutboundExternal;
        //    extensionViewModel.Recording_OutboundInternal = extension.Recording.OutboundInternal;
        //    extensionViewModel.Recording_Priority = extension.Recording.Priority.Value;

        //    extensionViewModel.cfringtimer = extension.ExtensionOptions.CallForwardRingTime.Value;
        //    extensionViewModel.callwaiting = extension.ExtensionOptions.CallWaiting;
        //    extensionViewModel.concurrency_limit = extension.ExtensionOptions.OutboundConcurrencyLimit.Value;
        //    extensionViewModel.ringtimer = extension.ExtensionOptions.RingTime.Value;


        //    if (extension.Voicemail.Enabled == "yes")
        //    {
        //        extensionViewModel.Voicemail_Voicemail = "yes";
        //        extensionViewModel.Voicemail_Password = extension.Voicemail.Pin.Value;
        //        extensionViewModel.Voicemail_EmailAddress = extension.Voicemail.Email;
        //        extensionViewModel.Voicemail_EmailAttachmen = extension.Voicemail.Options.EmailAttachment;
        //        extensionViewModel.Voicemail_PlayCID = extension.Voicemail.Options.PlayCallerid;
        //        extensionViewModel.Voicemail_PlayEnvelope = extension.Voicemail.Options.PlayEnvelope;
        //        extensionViewModel.Voicemail_DeleteVoicemail = extension.Voicemail.Options.Delete;
        //    }
        //    else
        //    {
        //        extensionViewModel.Voicemail_Voicemail = "no";
        //        extensionViewModel.Voicemail_Password = 2222;
        //        extensionViewModel.Voicemail_EmailAddress = "aa@bb.net";
        //        extensionViewModel.Voicemail_EmailAttachmen = "no";
        //        extensionViewModel.Voicemail_PlayCID = "no";
        //        extensionViewModel.Voicemail_PlayEnvelope = "no";
        //        extensionViewModel.Voicemail_DeleteVoicemail = "no";
        //    }
        //    //extensionViewModel.Voicemail_Voicemail = "selected";
        //    //extensionViewModel.Voicemail_Password = 2222;
        //    //extensionViewModel.Voicemail_EmailAddress = "aa@bb.net";
        //    //extensionViewModel.Voicemail_EmailAttachmen = "yes";
        //    //extensionViewModel.Voicemail_PlayCID = "yes";
        //    //extensionViewModel.Voicemail_PlayEnvelope = "yes";
        //    //extensionViewModel.Voicemail_DeleteVoicemail = "yes";


        //    //return null;
        //    return extensionViewModel;


        //}
        //public async Task<string> NewExtensionRestApi(ExtensionApiViewModel extensionViewModel)
        //{
        //    byte[] bytes = Encoding.UTF8.GetBytes(extensionViewModel.Name); ;
        //    string token = await GetNewTokenRest();
        //    ExtensionApiClass extension = new ExtensionApiClass();
        //    extension.Extension = extensionViewModel.Extension;
        //    extension.Name = Encoding.UTF8.GetString(bytes);
        //    extension.Secret = extensionViewModel.Secret;
        //    DeviceOptions extensiondeviceoptions = new DeviceOptions();
        //    extensiondeviceoptions.DtmfMode = extensionViewModel.DeviceOptions_DtmfMode;
        //    extensiondeviceoptions.Encryption = extensionViewModel.DeviceOptions_Encryption;
        //    extensiondeviceoptions.Nat = extensionViewModel.DeviceOptions_Nat;
        //    extensiondeviceoptions.Port = extensionViewModel.DeviceOptions_Port;
        //    extensiondeviceoptions.Transport = extensionViewModel.DeviceOptions_Transport;
        //    Recording extensionrecording = new Recording();
        //    extensionrecording.InboundExternal = extensionViewModel.Recording_InboundExternal;
        //    extensionrecording.InboundInternal = extensionViewModel.Recording_InboundInternal;
        //    extensionrecording.OutboundExternal = extensionViewModel.Recording_OutboundExternal;
        //    extensionrecording.OutboundInternal = extensionViewModel.Recording_OutboundInternal;
        //    extensionrecording.Priority = extensionViewModel.Recording_Priority;
        //    extension.DeviceOptions = extensiondeviceoptions;
        //    extension.Recording = extensionrecording;

        //    ExtensionOptions extensionOptions = new ExtensionOptions();
        //    extensionOptions.CallForwardRingTime = extensionViewModel.cfringtimer;
        //    extensionOptions.CallWaiting = extensionViewModel.callwaiting;
        //    extensionOptions.OutboundConcurrencyLimit = extensionViewModel.concurrency_limit;
        //    extensionOptions.RingTime = extensionViewModel.ringtimer;
        //    extension.ExtensionOptions = extensionOptions;

        //    if (extensionViewModel.Voicemail_Voicemail == "yes")
        //    {

        //        Voicemail exvoicemail = new Voicemail();



        //        exvoicemail.Enabled = "yes";
        //        exvoicemail.Pin = extensionViewModel.Voicemail_Password;
        //        exvoicemail.Email = extensionViewModel.Voicemail_EmailAddress;

        //        Options exvoiceopt = new Options();
        //        exvoiceopt.EmailAttachment = extensionViewModel.Voicemail_EmailAttachmen;
        //        exvoiceopt.PlayCallerid = extensionViewModel.Voicemail_PlayCID;
        //        exvoiceopt.PlayEnvelope = extensionViewModel.Voicemail_PlayEnvelope;
        //        exvoiceopt.Delete = extensionViewModel.Voicemail_DeleteVoicemail;


        //        extension.Voicemail = exvoicemail;
        //        extension.Voicemail.Options = exvoiceopt;
        //    }
        //    else
        //    {
        //        //extension.Voicemail.Enabled = "no";

        //    }


        //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
        //    HttpClient client = new HttpClient();
        //    string json = extension.ToJson();
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    string postadd = "https://" + IssabeLIP + "/pbxapi/extensions/" + extensionViewModel.Extension.ToString();
        //    var response = await client.PutAsync(postadd, content);
        //    string res = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        res = await response.Content.ReadAsStringAsync();
        //        res = "true";
        //    }
        //    else
        //    {
        //        res = "false";
        //    }
        //    //if (res.Contains("Created"))
        //    //{
        //    //    res = "true";
        //    //}
        //    //else
        //    //{
        //    //    res = "false";
        //    //}

        //    return res;
        //}
        public async Task<string> GetNewTokenRest()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(delegate { return true; });
            int retry = 0;
            retry:
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://" + IssabeLIP + "/pbxapi/authenticate/");

            var requestContent = string.Format("user={0}&password={1}", Uri.EscapeDataString(IssabeLUser),
                Uri.EscapeDataString(IssabeLPassword));

            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.SendAsync(request);
            string res = await response.Content.ReadAsStringAsync();

            if (res.Contains("access_token"))
            {
                return AccessTokenApi.FromJson(res).AccessToken;
            }
            else
            {
                if (retry <= 5)
                {
                    goto retry;
                }
                else
                {
                    return null;

                }


            }

        }
    }
}