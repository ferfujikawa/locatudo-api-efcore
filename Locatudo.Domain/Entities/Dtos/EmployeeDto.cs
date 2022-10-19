﻿namespace Locatudo.Domain.Entities.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
