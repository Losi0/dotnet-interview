using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos;

public class CreateTodoItemDto
{
 
    public required string Description { get; set; }
} 