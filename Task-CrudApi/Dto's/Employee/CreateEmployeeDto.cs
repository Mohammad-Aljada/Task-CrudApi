﻿namespace Task_CrudApi.Dto_s.Employee
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DepartmentId { get; set; }
    }
}
