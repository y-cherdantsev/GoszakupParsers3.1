create table contracts_goszakup
(
    id                        integer not null
        constraint contracts_goszakup_pk
            primary key,
    parent_id                 integer,
    root_id                   integer,
    trd_buy_id                integer,
    trd_buy_number_anno       text,
    ref_contract_status_id    integer,
    deleted                   boolean,
    crdate                    timestamp,
    last_update_date          timestamp,
    supplier_id               integer,
    supplier_biin             bigint,
    supplier_bik              text,
    supplier_iik              text,
    supplier_bank_name_kz     text,
    supplier_bank_name_ru     text,
    contract_number           text,
    sign_reason_doc_name      text,
    sign_reason_doc_date      timestamp,
    trd_buy_itogi_date_public timestamp,
    customer_id               integer,
    customer_bin              bigint,
    customer_bik              text,
    customer_iik              text,
    customer_bank_name_kz     text,
    customer_bank_name_ru     text,
    contract_number_sys       text,
    fin_year                  integer,
    ref_contract_agr_form_id  integer,
    ref_contract_year_type_id integer,
    ref_finsource_id          integer,
    ref_currency_code         text,
    contract_sum_wnds         double precision,
    sign_date                 timestamp,
    ec_end_date               timestamp,
    plan_exec_date            timestamp,
    fakt_sum_wnds             double precision,
    contract_end_date         timestamp,
    ref_contract_cancel_id    integer,
    ref_contract_type_id      integer,
    description_kz            text,
    description_ru            text,
    fakt_trade_methods_id     integer,
    ec_customer_approve       boolean,
    ec_supplier_approve       boolean,
    contract_ms               double precision,
    supplier_legal_address    text,
    customer_legal_address    text,
    payments_terms_ru         text,
    payments_terms_kz         text,
    is_gu                     boolean,
    exchange_rate             double precision,
    system_id                 integer,
    index_date                timestamp,
    relevance                 timestamp default CURRENT_TIMESTAMP,
    fakt_exec_date            timestamp
);

comment on table contracts_goszakup is 'Полная информация по договорам
https://ows.goszakup.gov.kz/v3/contract/all';

comment on column contracts_goszakup.id is 'Идентификатор';

comment on column contracts_goszakup.parent_id is 'Ид предыдущего договора';

comment on column contracts_goszakup.root_id is 'Ид корневого договора';

comment on column contracts_goszakup.trd_buy_id is 'Ид Объявления';

comment on column contracts_goszakup.trd_buy_number_anno is 'Номер объявления';

comment on column contracts_goszakup.ref_contract_status_id is 'Статус';

comment on column contracts_goszakup.deleted is 'Флаг удаления записи';

comment on column contracts_goszakup.crdate is 'Дата создания записи';

comment on column contracts_goszakup.last_update_date is 'Дата изменения записи';

comment on column contracts_goszakup.supplier_id is 'ИД Поставщика';

comment on column contracts_goszakup.supplier_biin is 'БИН/ИИН Поставщика';

comment on column contracts_goszakup.supplier_bik is 'БИК поставщика';

comment on column contracts_goszakup.supplier_iik is 'ИИК поставщика';

comment on column contracts_goszakup.supplier_bank_name_kz is 'Наименвоание банка поставщика на казахском языке';

comment on column contracts_goszakup.supplier_bank_name_ru is 'Наименвоание банка поставщика на русском языке';

comment on column contracts_goszakup.contract_number is 'Номер договора, заполняемый пользователем';

comment on column contracts_goszakup.sign_reason_doc_name is 'Наименование подтверждающего документа';

comment on column contracts_goszakup.sign_reason_doc_date is 'Дата подтверждающего документа';

comment on column contracts_goszakup.trd_buy_itogi_date_public is 'Дата подведения итогов госзакупок';

comment on column contracts_goszakup.customer_id is 'ИД заказчика ТРУ';

comment on column contracts_goszakup.customer_bin is 'БИН заказчика ТРУ';

comment on column contracts_goszakup.customer_bik is 'БИК заказчика';

comment on column contracts_goszakup.customer_iik is 'ИИК заказчика';

comment on column contracts_goszakup.customer_bank_name_kz is 'Наименвоание банка заказчика на казахском языке';

comment on column contracts_goszakup.customer_bank_name_ru is 'Наименвоание банка заказчика на русском языке';

comment on column contracts_goszakup.contract_number_sys is 'Номер договора в системе';

comment on column contracts_goszakup.fin_year is 'Финансовый год';

comment on column contracts_goszakup.ref_contract_agr_form_id is 'Форма заключения договора';

comment on column contracts_goszakup.ref_contract_year_type_id is 'Тип закупки (Тип закупки)';

comment on column contracts_goszakup.ref_finsource_id is 'Источник финансирования';

comment on column contracts_goszakup.ref_currency_code is 'Код валюты договора';

comment on column contracts_goszakup.contract_sum_wnds is 'Общая сумма договора, тенге';

comment on column contracts_goszakup.sign_date is 'Дата заключения договора';

comment on column contracts_goszakup.ec_end_date is 'Срок действия договора';

comment on column contracts_goszakup.plan_exec_date is 'Планируемая дата исполнения';

comment on column contracts_goszakup.fakt_sum_wnds is 'Общая фактическая сумма договора';

comment on column contracts_goszakup.contract_end_date is 'Дата расторжения договора';

comment on column contracts_goszakup.ref_contract_cancel_id is 'Основание и причина';

comment on column contracts_goszakup.ref_contract_type_id is 'Тип договора';

comment on column contracts_goszakup.description_kz is 'Описание на казахском языке';

comment on column contracts_goszakup.description_ru is 'Описание на русском языке';

comment on column contracts_goszakup.fakt_trade_methods_id is 'Фактический способ закупки';

comment on column contracts_goszakup.ec_customer_approve is 'Флаг “Согласован поставщиком”';

comment on column contracts_goszakup.ec_supplier_approve is 'Флаг “Согласован заказчиком”';

comment on column contracts_goszakup.contract_ms is '?Итоговая доля местного содержания по всему договору МСт (итоговая) (Местное содержание по договору, %)';

comment on column contracts_goszakup.supplier_legal_address is 'Юридический адрес поставщика';

comment on column contracts_goszakup.customer_legal_address is 'Юридический адрес заказчика';

comment on column contracts_goszakup.payments_terms_ru is 'Условия поставки на русском языке';

comment on column contracts_goszakup.payments_terms_kz is 'Условия поставки на государственном языке';

comment on column contracts_goszakup.is_gu is 'Признак ГУ';

comment on column contracts_goszakup.exchange_rate is '?Курс валюты (для валютных договоров)';

comment on column contracts_goszakup.system_id is 'ИД системы';

comment on column contracts_goszakup.index_date is 'Дата индексации';

comment on column contracts_goszakup.fakt_exec_date is 'Фактическая дата исполнения';

create unique index contracts_goszakup_id_uindex
    on contracts_goszakup (id);