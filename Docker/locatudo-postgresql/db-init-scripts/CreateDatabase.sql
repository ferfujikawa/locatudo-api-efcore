CREATE DATABASE locatudo;

\c locatudo;

CREATE TABLE public.departments (
	id uuid NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL
);

CREATE TABLE public.equipments (
	id uuid NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	manager_id uuid,
	CONSTRAINT fk_equipment_manager FOREIGN KEY (manager_id) REFERENCES public.departments (id) ON UPDATE CASCADE ON DELETE RESTRICT
);

CREATE TABLE public.users (
	id uuid NOT NULL PRIMARY KEY,
	first_name VARCHAR(100) NOT NULL,
	last_name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL
);

CREATE TABLE public.employees (
	id uuid NOT NULL PRIMARY KEY,
	department_id uuid NOT NULL,
	CONSTRAINT fk_employee_user FOREIGN KEY (id) REFERENCES public.users (id) ON UPDATE CASCADE ON DELETE RESTRICT,
	CONSTRAINT fk_employee_department FOREIGN KEY (department_id) REFERENCES public.departments (id) ON UPDATE CASCADE ON DELETE RESTRICT
);

CREATE TABLE public.outsourceds (
	id uuid NOT NULL PRIMARY KEY,
	company_name VARCHAR(100) NOT NULL,
	CONSTRAINT fk_outsourced_user FOREIGN KEY (id) REFERENCES public.users (id) ON UPDATE CASCADE ON DELETE RESTRICT
);

CREATE TABLE public.rentals (
	id uuid NOT NULL PRIMARY KEY,
	tenant_id uuid NOT NULL,
	equipment_id uuid NOT NULL,
	time DATE NOT NULL,
	status INT NOT NUll,
	appraiser_id uuid,
	CONSTRAINT fk_rental_tenant FOREIGN KEY (tenant_id) REFERENCES public.users (id) ON UPDATE CASCADE ON DELETE RESTRICT,
	CONSTRAINT fk_rental_equipment FOREIGN KEY (equipment_id) REFERENCES public.equipments (id) ON UPDATE CASCADE ON DELETE RESTRICT,
	CONSTRAINT fk_rental_appraiser FOREIGN KEY (appraiser_id) REFERENCES public.employees (id) ON UPDATE CASCADE ON DELETE RESTRICT
);

INSERT INTO public.departments (id, name, email)
     VALUES ('b8aac76d-1feb-4a1a-86f0-38447ca2a444', 'Departamento 1', 'departamento1@locatudo.com.br'),
		('eb8b2723-953b-4844-a3c4-a0486d122476', 'Departamento 2', 'departamento2@locatudo.com.br'),
		('388cf623-9011-47f9-afd8-7027cb73111f', 'Departamento 3', 'departamento3@locatudo.com.br');

INSERT INTO public.equipments (id, name, manager_id)
     VALUES ('df738853-6246-4c8f-a3bc-51f0558ac1ac', 'Equipamento 1', '388cf623-9011-47f9-afd8-7027cb73111f'),
		('7d87dc84-fef1-4027-b8cf-4e361421f831', 'Equipamento 2', 'b8aac76d-1feb-4a1a-86f0-38447ca2a444'),
		('ce46ba1a-1a83-4083-974d-d47c8db72013', 'Equipamento 3', 'eb8b2723-953b-4844-a3c4-a0486d122476');

INSERT INTO public.users (id, first_name, last_name, email)
     VALUES ('b10fe568-056d-4e82-b6c7-34e75761befe', 'Funcionário 1', 'Sobrenome', 'funcionario1@locatudo.com.br'),
		('c8b06501-4aea-4f72-baf3-baee5a6ba9cc', 'Funcionário 2', 'Sobrenome', 'funcionario2@locatudo.com.br'),
		('7865b629-f8c9-4e0a-abca-075bdc03c4b7', 'Terceirizado 1', 'Sobrenome', 'terceirizado1@empresa1.com.br'),
		('840fe470-d0ac-4ee7-bd09-efe748cf3957', 'Terceirizado 2', 'Sobrenome', 'terceirizado2@empresa1.com.br');

INSERT INTO public.employees (id, department_id)
     VALUES ('b10fe568-056d-4e82-b6c7-34e75761befe', '388cf623-9011-47f9-afd8-7027cb73111f'),
		('c8b06501-4aea-4f72-baf3-baee5a6ba9cc', 'b8aac76d-1feb-4a1a-86f0-38447ca2a444');

INSERT INTO public.outsourceds (id, company_name)
     VALUES ('7865b629-f8c9-4e0a-abca-075bdc03c4b7', 'Empresa 1'),
		('840fe470-d0ac-4ee7-bd09-efe748cf3957', 'Empresa 1');