create table lots_goszakup
(
    id                   integer                             not null
        constraint lots_goszakup_pk
            primary key,
    lot_number           text,
    ref_lot_status_id    integer,
    last_update_date     timestamp,
    union_lots           boolean,
    count                double precision,
    amount               double precision,
    name_ru              text,
    name_kz              text,
    description_ru       text,
    description_kz       text,
    customer_id          integer,
    customer_bin         bigint,
    trd_buy_number_anno  text,
    trd_buy_id           integer,
    dumping              boolean,
    dumping_lot_price    integer,
    psd_sign             smallint,
    consulting_services  boolean,
    is_light_industry    boolean,
    is_construction_work boolean,
    disable_person_id    boolean,
    customer_name_kz     text,
    customer_name_ru     text,
    ref_trade_methods_id integer,
    index_date           timestamp,
    system_id            integer,
    single_org_sign      smallint,
    relevance            timestamp default CURRENT_TIMESTAMP not null
);

comment on table lots_goszakup is 'Реестр лотов
https://ows.goszakup.gov.kz/v3/lots';

comment on column lots_goszakup.id is 'ИД лота';

comment on column lots_goszakup.lot_number is 'Номер лота';

comment on column lots_goszakup.ref_lot_status_id is 'Статус лота';

comment on column lots_goszakup.last_update_date is 'Дата последнего изменения';

comment on column lots_goszakup.union_lots is 'Признак объединенного лота';

comment on column lots_goszakup.count is 'Общее количество';

comment on column lots_goszakup.amount is 'Общая сумма';

comment on column lots_goszakup.name_ru is 'Наименование на русском языке';

comment on column lots_goszakup.name_kz is 'Наименование на государственном языке';

comment on column lots_goszakup.description_ru is 'Детальное описание на русском языке';

comment on column lots_goszakup.description_kz is 'Детальное описание на государственном языке';

comment on column lots_goszakup.customer_id is 'Идентификатор заказчика';

comment on column lots_goszakup.customer_bin is 'БИН заказчика';

comment on column lots_goszakup.trd_buy_number_anno is 'Номер объявления';

comment on column lots_goszakup.trd_buy_id is 'Уникальный идентификатор объявления';

comment on column lots_goszakup.dumping is '?Признак демпинга';

comment on column lots_goszakup.dumping_lot_price is '?Сумма для расчета демпинга';

comment on column lots_goszakup.psd_sign is 'Признак работы. 1-работа с ТЭО/ПСД, 2-работа на разработку ТЭО/ПСД';

comment on column lots_goszakup.consulting_services is 'Признак Консультационная услуга';

comment on column lots_goszakup.is_light_industry is 'Закупка легкой и мебельной промышленности';

comment on column lots_goszakup.is_construction_work is 'Закупка с признаком СМР';

comment on column lots_goszakup.disable_person_id is 'Признак - Закупка среди организаций инвалидов';

comment on column lots_goszakup.customer_name_kz is 'Наименование заказчика на государственном языке';

comment on column lots_goszakup.customer_name_ru is 'Наименование заказчика на русском языке';

comment on column lots_goszakup.ref_trade_methods_id is 'ИД Способа закупки';

comment on column lots_goszakup.index_date is 'Дата индексации';

comment on column lots_goszakup.system_id is 'Уникальный идентификатор системы';

comment on column lots_goszakup.single_org_sign is 'Закупки Единого организатора КГЗ МФ РК';

comment on column lots_goszakup.relevance is 'Локально - Релевантность записи';

create unique index lots_goszakup_id_uindex
    on lots_goszakup (id);