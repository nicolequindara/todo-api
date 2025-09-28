namespace Todo.Models;

public class Todo
{
    /// <summary>
    /// unique identifier
    /// </summary>
    public  int Id { get; set; }
    
    /// <summary>
    /// required title of task
    /// </summary>
    public String Title { get; set; }
    
    /// <summary>
    /// optional description
    /// </summary>
    public String? Description { get; set; }
    
    /// <summary>
    /// optional due date of task
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    /// date on which task was created
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// status of task - defaults to Active
    /// </summary>
    public TodoStatus Status { get; set; } = TodoStatus.Active;
}