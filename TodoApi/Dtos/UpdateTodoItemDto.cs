using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos;

public class UpdateTodoItemDto
{

    public required string Description { get; set; }
} 