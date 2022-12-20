using System.Text.Json.Serialization;
using EntityContext.Fms.Wrapper;

namespace EntityContext.Fms
{
    public class RobotState : FmsWrapper
    {
        public RobotState() : base(typeof(RobotState))
        {
            RobotId = "";
        }

        protected override List<string>? KeyColumns => new List<string>(){
            "robot_id"
        };

        [JsonInclude]
        public string RobotId { get; set; }
        [JsonInclude]
        public string? TaskId { get; set; }
        [JsonInclude]
        public string? RobotMode { get; set; }
        [JsonInclude]
        public string? GoalNodeId { get; set; }
        [JsonInclude]
        public Decimal? PoseX { get; set; }
        [JsonInclude]
        public Decimal? PoseY { get; set; }
        [JsonInclude]
        public Decimal? PoseZ { get; set; }
        [JsonInclude]
        public Decimal? TargetX { get; set; }
        [JsonInclude]
        public Decimal? TargetY { get; set; }
        [JsonInclude]
        public Decimal? TargetZ { get; set; }
        [JsonInclude]
        public Decimal? Progress { get; set; }
        [JsonInclude]
        public Decimal? Battery { get; set; }
        [JsonInclude]
        public string? ReadDate { get; set; }
        [JsonInclude]
        public Decimal? PoseTheta { get; set; }

        // [JsonInclude]
        // [JsonPropertyName("robot_id")]
        // public string RobotId { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("task_id")]
        // public string? TaskId { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("robot_mode")]
        // public string? RobotMode { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("goal_node_id")]
        // public string? GoalNodeId { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("pose_x")]
        // public Decimal? PoseX { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("pose_y")]
        // public Decimal? PoseY { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("pose_z")]
        // public Decimal? PoseZ { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("target_x")]
        // public Decimal? TargetX { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("target_y")]
        // public Decimal? TargetY { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("target_z")]
        // public Decimal? TargetZ { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("progress")]
        // public Decimal? Progress { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("battery")]
        // public Decimal? Battery { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("read_date")]
        // public string? ReadDate { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("pose_theta")]
        // public Decimal? PoseTheta { get; set; }
    }
}
