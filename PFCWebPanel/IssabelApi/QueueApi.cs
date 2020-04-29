using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PFCWebPanel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PFCWebPanel.QueueApiClass
{
    public partial class QueueApi
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("dynamic_members", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> DynamicMembers { get; set; }

        [JsonProperty("restrict_dynamic_agents", NullValueHandling = NullValueHandling.Ignore)]
        public string RestrictDynamicAgents { get; set; }

        [JsonProperty("static_members", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> StaticMembers { get; set; }

        [JsonProperty("extension", NullValueHandling = NullValueHandling.Ignore)]
        public string Extension { get; set; }

        [JsonProperty("alert_info", NullValueHandling = NullValueHandling.Ignore)]
        public string AlertInfo { get; set; }

        [JsonProperty("music_on_hold_ringing", NullValueHandling = NullValueHandling.Ignore)]
        public string MusicOnHoldRinging { get; set; }

        [JsonProperty("max_wait", NullValueHandling = NullValueHandling.Ignore)]
        public string MaxWait { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("breakout_ivr_id", NullValueHandling = NullValueHandling.Ignore)]
        public string BreakoutIvrId { get; set; }

        [JsonProperty("destination", NullValueHandling = NullValueHandling.Ignore)]
        public string Destination { get; set; }

        [JsonProperty("skip_busy_agents", NullValueHandling = NullValueHandling.Ignore)]
        public string SkipBusyAgents { get; set; }

        [JsonProperty("queue_regular_expression", NullValueHandling = NullValueHandling.Ignore)]
        public string QueueRegularExpression { get; set; }

        [JsonProperty("agent_announce_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AgentAnnounceId { get; set; }

        [JsonProperty("join_announce_id", NullValueHandling = NullValueHandling.Ignore)]
        public string JoinAnnounceId { get; set; }

        [JsonProperty("wait_time_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string WaitTimePrefix { get; set; }

        [JsonProperty("agent_restrictions", NullValueHandling = NullValueHandling.Ignore)]
        public string AgentRestrictions { get; set; }

        [JsonProperty("generate_device_hints", NullValueHandling = NullValueHandling.Ignore)]
        public string GenerateDeviceHints { get; set; }

        [JsonProperty("queue_no_answer", NullValueHandling = NullValueHandling.Ignore)]
        public string QueueNoAnswer { get; set; }

        [JsonProperty("call_confirm", NullValueHandling = NullValueHandling.Ignore)]
        public string CallConfirm { get; set; }

        [JsonProperty("call_confirm_announce_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CallConfirmAnnounceId { get; set; }

        [JsonProperty("monitor_type", NullValueHandling = NullValueHandling.Ignore)]
        public string MonitorType { get; set; }

        [JsonProperty("monitor_heard", NullValueHandling = NullValueHandling.Ignore)]
        public string MonitorHeard { get; set; }

        [JsonProperty("monitor_spoken", NullValueHandling = NullValueHandling.Ignore)]
        public string MonitorSpoken { get; set; }

        [JsonProperty("callback_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CallbackId { get; set; }

        [JsonProperty("destination_on_continue", NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationOnContinue { get; set; }

        [JsonProperty("cid_name_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string CidNamePrefix { get; set; }

        [JsonProperty("announce_frequency", NullValueHandling = NullValueHandling.Ignore)]
        public string AnnounceFrequency { get; set; }

        [JsonProperty("announce_holdtime", NullValueHandling = NullValueHandling.Ignore)]
        public string AnnounceHoldtime { get; set; }

        [JsonProperty("announce_position", NullValueHandling = NullValueHandling.Ignore)]
        public string AnnouncePosition { get; set; }

        [JsonProperty("answered_elsewhere", NullValueHandling = NullValueHandling.Ignore)]
        public string AnsweredElsewhere { get; set; }

        [JsonProperty("autofill", NullValueHandling = NullValueHandling.Ignore)]
        public string Autofill { get; set; }

        [JsonProperty("auto_pause", NullValueHandling = NullValueHandling.Ignore)]
        public string AutoPause { get; set; }

        [JsonProperty("auto_pause_if_busy", NullValueHandling = NullValueHandling.Ignore)]
        public string AutoPauseIfBusy { get; set; }

        [JsonProperty("auto_pause_delay", NullValueHandling = NullValueHandling.Ignore)]
        public string AutoPauseDelay { get; set; }

        [JsonProperty("auto_pause_if_unavailable", NullValueHandling = NullValueHandling.Ignore)]
        public string AutoPauseIfUnavailable { get; set; }

        [JsonProperty("cron_schedule", NullValueHandling = NullValueHandling.Ignore)]
        public string CronSchedule { get; set; }

        [JsonProperty("event_member_status", NullValueHandling = NullValueHandling.Ignore)]
        public string EventMemberStatus { get; set; }

        [JsonProperty("event_when_called", NullValueHandling = NullValueHandling.Ignore)]
        public string EventWhenCalled { get; set; }

        [JsonProperty("join_empty", NullValueHandling = NullValueHandling.Ignore)]
        public string JoinEmpty { get; set; }

        [JsonProperty("leave_when_empty", NullValueHandling = NullValueHandling.Ignore)]
        public string LeaveWhenEmpty { get; set; }

        [JsonProperty("max_callers_waiting", NullValueHandling = NullValueHandling.Ignore)]
        public string MaxCallersWaiting { get; set; }

        [JsonProperty("member_delay", NullValueHandling = NullValueHandling.Ignore)]
        public string MemberDelay { get; set; }

        [JsonProperty("monitor_format", NullValueHandling = NullValueHandling.Ignore)]
        public string MonitorFormat { get; set; }

        [JsonProperty("monitor_join", NullValueHandling = NullValueHandling.Ignore)]
        public string MonitorJoin { get; set; }

        [JsonProperty("penalty_members_limit", NullValueHandling = NullValueHandling.Ignore)]
        public string PenaltyMembersLimit { get; set; }

        [JsonProperty("periodic_announce_frequency", NullValueHandling = NullValueHandling.Ignore)]
        public string PeriodicAnnounceFrequency { get; set; }

        [JsonProperty("sound_calls_waiting", NullValueHandling = NullValueHandling.Ignore)]
        public string SoundCallsWaiting { get; set; }

        [JsonProperty("sound_thank_you", NullValueHandling = NullValueHandling.Ignore)]
        public string SoundThankYou { get; set; }

        [JsonProperty("sound_there_are", NullValueHandling = NullValueHandling.Ignore)]
        public string SoundThereAre { get; set; }

        [JsonProperty("sound_you_are_next", NullValueHandling = NullValueHandling.Ignore)]
        public string SoundYouAreNext { get; set; }

        [JsonProperty("report_hold_time", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportHoldTime { get; set; }

        [JsonProperty("agent_retry", NullValueHandling = NullValueHandling.Ignore)]
        public string AgentRetry { get; set; }

        [JsonProperty("ring_busy_members", NullValueHandling = NullValueHandling.Ignore)]
        public string RingBusyMembers { get; set; }

        [JsonProperty("service_level", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceLevel { get; set; }

        [JsonProperty("skip_join_announce", NullValueHandling = NullValueHandling.Ignore)]
        public string SkipJoinAnnounce { get; set; }

        [JsonProperty("ring_strategy", NullValueHandling = NullValueHandling.Ignore)]
        public string RingStrategy { get; set; }

        [JsonProperty("timeout", NullValueHandling = NullValueHandling.Ignore)]
        public string Timeout { get; set; }

        [JsonProperty("timeout_priority", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeoutPriority { get; set; }

        [JsonProperty("timeout_restart", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeoutRestart { get; set; }

        [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
        public string Weight { get; set; }

        [JsonProperty("wrapup_time", NullValueHandling = NullValueHandling.Ignore)]
        public string WrapupTime { get; set; }

        [JsonProperty("music_on_hold_class", NullValueHandling = NullValueHandling.Ignore)]
        public string MusicOnHoldClass { get; set; }
    }

    public partial class QueueApi
    {
        public static QueueApi FromJson(string json) => JsonConvert.DeserializeObject<QueueApi>(json, PFCWebPanel.QueueApiClass.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this QueueApi self) => JsonConvert.SerializeObject(self, PFCWebPanel.QueueApiClass.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
