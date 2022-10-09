CREATE DATABASE locatudo;

\c locatudo;

CREATE TABLE public.departments (
	id uuid NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL
);

CREATE TABLE public.equipments (
	id uuid NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL
);

CREATE TABLE public.users (
	id uuid NOT NULL PRIMARY KEY,
	first_name VARCHAR(100) NOT NULL,
	last_name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL
);

CREATE TABLE public.employees (
	department_id uuid NOT NULL,
	CONSTRAINT fk_employee_department FOREIGN KEY (department_id) REFERENCES public.departments (id) ON UPDATE CASCADE ON DELETE RESTRICT
) INHERITS (public.users);

CREATE TABLE public.outsourceds (
	company_name VARCHAR(100) NOT NULL
) INHERITS (public.users);

CREATE TABLE public.rentals (
	id uuid NOT NULL PRIMARY KEY,
	tenant_id uuid NOT NULL,
	equipment_id uuid NOT NULL,
	time DATE NOT NULL,
	status INT NOT NUll,
	appraiser_id uuid,
	CONSTRAINT fk_rental_tenant FOREIGN KEY (tenant_id) REFERENCES public.users (id) ON UPDATE CASCADE ON DELETE RESTRICT,
	CONSTRAINT fk_rental_equipment FOREIGN KEY (equipment_id) REFERENCES public.equipments (id) ON UPDATE CASCADE ON DELETE RESTRICT,
	CONSTRAINT fk_rental_appraiser FOREIGN KEY (appraiser_id) REFERENCES public.users (id) ON UPDATE CASCADE ON DELETE RESTRICT
);