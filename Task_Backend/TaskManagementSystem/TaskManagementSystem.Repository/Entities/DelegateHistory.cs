using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Repository.Entities
{
    public class DelegateHistory : CommonEntityModel
    {
        public int delegateHistoryId { get; set; }
        public string RaisedBy { get; set; }
        public int TaskId { get; set; }
        public int TransferToId { get; set; }
        public string TransferTo { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public bool isActive { get; set; }
        public string RejectRemarks { get; set; }
        public virtual TaskTransaction TaskTransaction { get; set; }
    }
}
