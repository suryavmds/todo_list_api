namespace ToDoListAPI.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public System.DateTime? DueDate { get; set; }
        public System.DateTime? CompletedDate { get; set; }
    }
}