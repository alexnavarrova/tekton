# Tekton

DDL:
CREATE DATABASE tekton;

use tekton;

CREATE TABLE public."Products" (
	product_id serial4 NOT NULL,
	"name" varchar(50) NOT NULL,
	status_id int2 NOT NULL,
	stock int4 NOT NULL,
	description varchar(100) NOT NULL,
	price numeric NOT NULL,
	sku varchar(20) NOT NULL,
	created_date timestamp DEFAULT now() NULL,
	created_by varchar NULL,
	last_modified_date varchar NULL,
	last_modified_by varchar NULL,
	CONSTRAINT products_pk PRIMARY KEY (product_id),
	CONSTRAINT products_unique UNIQUE (sku),
	CONSTRAINT products_productstatuses_fk FOREIGN KEY (status_id) REFERENCES public."ProductStatuses"(status_id)
);

CREATE TABLE public."ProductStatuses" (
	status_id int4 NOT NULL,
	description varchar NULL,
	created_date timestamp NULL,
	created_by varchar NULL,
	last_modified_date timestamp NULL,
	last_modified_by varchar NULL,
	CONSTRAINT "ProductStatus_pkey" PRIMARY KEY (status_id)
);

INSERT INTO public."ProductStatuses" (status_id, description) VALUES	 
	(0, 'Inactive'),
	(1, 'Active');

Configuration:
Change the values in appsetting