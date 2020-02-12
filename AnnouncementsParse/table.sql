create table announcements_table
(
    id                   integer not null
        constraint announcements_table_pk
            primary key,
    number_anno          text,
    name_ru              text,
    name_kz              text,
    total_sum            double precision,
    ref_trade_methods_id integer,
    ref_subject_type_id  integer,
    customer_bin         bigint,
    customer_pid         integer,
    org_bin              bigint,
    org_pid              integer,
    ref_buy_status_id    integer,
    start_date           timestamp,
    repeat_start_date    timestamp,
    repeat_end_date      timestamp,
    end_date             timestamp,
    publish_date         timestamp,
    itogi_date_public    timestamp,
    ref_type_trade_id    integer,
    disable_person_id    boolean,
    discus_start_date    timestamp,
    discus_end_date      timestamp,
    id_supplier          integer,
    biin_supplier        bigint,
    parent_id            integer,
    singl_org_sign       boolean,
    is_light_industry    boolean,
    is_construction_work boolean,
    customer_name_kz     text,
    customer_name_ru     text,
    org_name_kz          text,
    org_name_ru          text,
    system_id            integer,
    index_date           timestamp,
    relevance            timestamp default CURRENT_TIMESTAMP
);

comment on table announcements_table is 'Получение полного списка объявлений
https://ows.goszakup.gov.kz/v3/trd-buy/all';

comment on column announcements_table.id is 'ИД объявления';

comment on column announcements_table.number_anno is 'Номер объявления';

comment on column announcements_table.name_ru is 'Наименование на русском языке';

comment on column announcements_table.name_kz is 'Наименование на казахском языке';

comment on column announcements_table.total_sum is 'Общая сумма запланированная для закупки (Сумма закупки)';

comment on column announcements_table.ref_trade_methods_id is 'Код способа закупки';

comment on column announcements_table.ref_subject_type_id is 'Вид предмета закупок';

comment on column announcements_table.customer_bin is 'БИН Заказчика';

comment on column announcements_table.customer_pid is 'ИД Заказчика';

comment on column announcements_table.org_bin is 'БИН Организатора';

comment on column announcements_table.org_pid is 'ИД Организатора';

comment on column announcements_table.ref_buy_status_id is 'Статуса объявления';

comment on column announcements_table.start_date is 'Дата начала приема заявок';

comment on column announcements_table.repeat_start_date is 'Срок начала повторного предоставления (дополнения) заявок';

comment on column announcements_table.repeat_end_date is 'Срок окончания повторного предоставления (дополнения) заявок';

comment on column announcements_table.end_date is 'Дата окончания приема заявок';

comment on column announcements_table.publish_date is 'Дата и время публикации';

comment on column announcements_table.itogi_date_public is 'Дата публикации итогов';

comment on column announcements_table.ref_type_trade_id is 'Тип закупки (первая, повторная)';

comment on column announcements_table.disable_person_id is 'Признак закупки инвалиды';

comment on column announcements_table.discus_start_date is 'Срок начала обсуждения';

comment on column announcements_table.discus_end_date is 'Срок окончания обсуждения';

comment on column announcements_table.id_supplier is 'ID поставщика из одного источника';

comment on column announcements_table.biin_supplier is 'БИН/ИИН поставщика из одного источника';

comment on column announcements_table.parent_id is 'ИД исходного объявления';

comment on column announcements_table.singl_org_sign is 'Закупки Единого организатора КГЗ МФ РК';

comment on column announcements_table.is_light_industry is 'Закупка легкой и мебельной промышленности';

comment on column announcements_table.is_construction_work is 'Закупка с признаком СМР';

comment on column announcements_table.customer_name_kz is 'Наименование заказчика на государственном языке';

comment on column announcements_table.customer_name_ru is 'Наименование заказчика на русском языке';

comment on column announcements_table.org_name_kz is 'Наименование организатора на государственном языке';

comment on column announcements_table.org_name_ru is 'Наименование организатора на русском языке';

comment on column announcements_table.system_id is 'ИД системы';

comment on column announcements_table.index_date is 'Дата индексации';

comment on column announcements_table.relevance is 'Локально - Релевантность записи';

create unique index announcements_table_id_uindex
    on announcements_table (id);

