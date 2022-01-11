create database Comex;
use Comex;

create table exportacoes(
ano year not null,
mes tinyint not null,
codsh4 int not null,
pais int not null,
estado char(2) not null,
codMunicipio int not null,
peso bigint not null,
CONSTRAINT FK_exportacoes_sh4 foreign key (codsh4) references sh4(codSH4),
CONSTRAINT FK_exportacoes_codpais foreign key (pais) references codPais(codPais),
CONSTRAINT FK_exportacoes_codmunicipio foreign key (codMunicipio) references codmunicipio(codMunicipio)
);

create table ncm(
codNCM tinyint primary key not null,
descricaoSH4 text
);

create table sh4(
codSH4 tinyint primary key not null,
descricaoSH4 text,
codNCM tinyint not null,
CONSTRAINT FK_sh4_ncm foreign key (codNCM) references ncm(codNCM)
);

create table codPais(
codPais int primary key not null,
descricaoPais text
);

create table codMunicipio(
codMunicipio int primary key not null,
descricaoMunicipio text
);















