create table unscrupulouses_goszakup
(
    pid              integer                             not null
        constraint unscrupulouses_goszakup_pk
            primary key,
    supplier_biin    bigint,
    supplier_innunp  text,
    supplier_name_ru text,
    supplier_name_kz text,
    index_date       timestamp,
    system_id        integer,
    relevance        timestamp default CURRENT_TIMESTAMP not null
);

comment on table unscrupulouses_goszakup is 'Реестр недобросовестных поставщиков
https://ows.goszakup.gov.kz/v3/rnu';

comment on column unscrupulouses_goszakup.pid is 'ID участника';

comment on column unscrupulouses_goszakup.supplier_biin is 'БИН/ИИН Участника';

comment on column unscrupulouses_goszakup.supplier_innunp is 'ИНН/УНП Участника';

comment on column unscrupulouses_goszakup.supplier_name_ru is 'Наименование участника на русском языке';

comment on column unscrupulouses_goszakup.supplier_name_kz is 'Наименование участника на казахском языке';

comment on column unscrupulouses_goszakup.index_date is 'Дата индексации объекта';

comment on column unscrupulouses_goszakup.system_id is 'Идентификатор системы';

comment on column unscrupulouses_goszakup.relevance is 'Локально - Релевантность записи';

create unique index unscrupulouses_goszakup_pid_uindex
    on unscrupulouses_goszakup (pid);

