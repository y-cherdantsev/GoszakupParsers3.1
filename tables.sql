--announcement
create table announcement_goszakup
(
    id                   integer not null
        constraint announcement_goszakup_pk
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
    single_org_sign      boolean,
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

comment on table announcement_goszakup is 'Получение полного списка объявлений
https://ows.goszakup.gov.kz/v3/trd-buy/all';

comment on column announcement_goszakup.id is 'ИД объявления';

comment on column announcement_goszakup.number_anno is 'Номер объявления';

comment on column announcement_goszakup.name_ru is 'Наименование на русском языке';

comment on column announcement_goszakup.name_kz is 'Наименование на казахском языке';

comment on column announcement_goszakup.total_sum is 'Общая сумма запланированная для закупки (Сумма закупки)';

comment on column announcement_goszakup.ref_trade_methods_id is 'Код способа закупки';

comment on column announcement_goszakup.ref_subject_type_id is 'Вид предмета закупок';

comment on column announcement_goszakup.customer_bin is 'БИН Заказчика';

comment on column announcement_goszakup.customer_pid is 'ИД Заказчика';

comment on column announcement_goszakup.org_bin is 'БИН Организатора';

comment on column announcement_goszakup.org_pid is 'ИД Организатора';

comment on column announcement_goszakup.ref_buy_status_id is 'Статуса объявления';

comment on column announcement_goszakup.start_date is 'Дата начала приема заявок';

comment on column announcement_goszakup.repeat_start_date is 'Срок начала повторного предоставления (дополнения) заявок';

comment on column announcement_goszakup.repeat_end_date is 'Срок окончания повторного предоставления (дополнения) заявок';

comment on column announcement_goszakup.end_date is 'Дата окончания приема заявок';

comment on column announcement_goszakup.publish_date is 'Дата и время публикации';

comment on column announcement_goszakup.itogi_date_public is 'Дата публикации итогов';

comment on column announcement_goszakup.ref_type_trade_id is 'Тип закупки (первая, повторная)';

comment on column announcement_goszakup.disable_person_id is 'Признак закупки инвалиды';

comment on column announcement_goszakup.discus_start_date is 'Срок начала обсуждения';

comment on column announcement_goszakup.discus_end_date is 'Срок окончания обсуждения';

comment on column announcement_goszakup.id_supplier is 'ID поставщика из одного источника';

comment on column announcement_goszakup.biin_supplier is 'БИН/ИИН поставщика из одного источника';

comment on column announcement_goszakup.parent_id is 'ИД исходного объявления';

comment on column announcement_goszakup.single_org_sign is 'Закупки Единого организатора КГЗ МФ РК';

comment on column announcement_goszakup.is_light_industry is 'Закупка легкой и мебельной промышленности';

comment on column announcement_goszakup.is_construction_work is 'Закупка с признаком СМР';

comment on column announcement_goszakup.customer_name_kz is 'Наименование заказчика на государственном языке';

comment on column announcement_goszakup.customer_name_ru is 'Наименование заказчика на русском языке';

comment on column announcement_goszakup.org_name_kz is 'Наименование организатора на государственном языке';

comment on column announcement_goszakup.org_name_ru is 'Наименование организатора на русском языке';

comment on column announcement_goszakup.system_id is 'ИД системы';

comment on column announcement_goszakup.index_date is 'Дата индексации';

comment on column announcement_goszakup.relevance is 'Локально - Релевантность записи';

create unique index announcement_goszakup_id_uindex
    on announcement_goszakup (id);


--contract
create table contract_goszakup
(
    id                        integer not null
        constraint contract_goszakup_pk
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
    fin_year                  date,
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
    fakt_exec_date            timestamp,
    relevance                 timestamp default CURRENT_TIMESTAMP
);

comment on table contract_goszakup is 'Полная информация по договорам
https://ows.goszakup.gov.kz/v3/contract/all';

comment on column contract_goszakup.id is 'Идентификатор';

comment on column contract_goszakup.parent_id is 'Ид предыдущего договора';

comment on column contract_goszakup.root_id is 'Ид корневого договора';

comment on column contract_goszakup.trd_buy_id is 'Ид Объявления';

comment on column contract_goszakup.trd_buy_number_anno is 'Номер объявления';

comment on column contract_goszakup.ref_contract_status_id is 'Статус';

comment on column contract_goszakup.deleted is 'Флаг удаления записи';

comment on column contract_goszakup.crdate is 'Дата создания записи';

comment on column contract_goszakup.last_update_date is 'Дата изменения записи';

comment on column contract_goszakup.supplier_id is 'ИД Поставщика';

comment on column contract_goszakup.supplier_biin is 'БИН/ИИН Поставщика';

comment on column contract_goszakup.supplier_bik is 'БИК поставщика';

comment on column contract_goszakup.supplier_iik is 'ИИК поставщика';

comment on column contract_goszakup.supplier_bank_name_kz is 'Наименвоание банка поставщика на казахском языке';

comment on column contract_goszakup.supplier_bank_name_ru is 'Наименвоание банка поставщика на русском языке';

comment on column contract_goszakup.contract_number is 'Номер договора, заполняемый пользователем';

comment on column contract_goszakup.sign_reason_doc_name is 'Наименование подтверждающего документа';

comment on column contract_goszakup.sign_reason_doc_date is 'Дата подтверждающего документа';

comment on column contract_goszakup.trd_buy_itogi_date_public is 'Дата подведения итогов госзакупок';

comment on column contract_goszakup.customer_id is 'ИД заказчика ТРУ';

comment on column contract_goszakup.customer_bin is 'БИН заказчика ТРУ';

comment on column contract_goszakup.customer_bik is 'БИК заказчика';

comment on column contract_goszakup.customer_iik is 'ИИК заказчика';

comment on column contract_goszakup.customer_bank_name_kz is 'Наименвоание банка заказчика на казахском языке';

comment on column contract_goszakup.customer_bank_name_ru is 'Наименвоание банка заказчика на русском языке';

comment on column contract_goszakup.contract_number_sys is 'Номер договора в системе';

comment on column contract_goszakup.fin_year is 'Финансовый год';

comment on column contract_goszakup.ref_contract_agr_form_id is 'Форма заключения договора';

comment on column contract_goszakup.ref_contract_year_type_id is 'Тип закупки (Тип закупки)';

comment on column contract_goszakup.ref_finsource_id is 'Источник финансирования';

comment on column contract_goszakup.ref_currency_code is 'Код валюты договора';

comment on column contract_goszakup.contract_sum_wnds is 'Общая сумма договора, тенге';

comment on column contract_goszakup.sign_date is 'Дата заключения договора';

comment on column contract_goszakup.ec_end_date is 'Срок действия договора';

comment on column contract_goszakup.plan_exec_date is 'Планируемая дата исполнения';

comment on column contract_goszakup.fakt_sum_wnds is 'Общая фактическая сумма договора';

comment on column contract_goszakup.contract_end_date is 'Дата расторжения договора';

comment on column contract_goszakup.ref_contract_cancel_id is 'Основание и причина';

comment on column contract_goszakup.ref_contract_type_id is 'Тип договора';

comment on column contract_goszakup.description_kz is 'Описание на казахском языке';

comment on column contract_goszakup.description_ru is 'Описание на русском языке';

comment on column contract_goszakup.fakt_trade_methods_id is 'Фактический способ закупки';

comment on column contract_goszakup.ec_customer_approve is 'Флаг “Согласован поставщиком”';

comment on column contract_goszakup.ec_supplier_approve is 'Флаг “Согласован заказчиком”';

comment on column contract_goszakup.contract_ms is '?Итоговая доля местного содержания по всему договору МСт (итоговая) (Местное содержание по договору, %)';

comment on column contract_goszakup.supplier_legal_address is 'Юридический адрес поставщика';

comment on column contract_goszakup.customer_legal_address is 'Юридический адрес заказчика';

comment on column contract_goszakup.payments_terms_ru is 'Условия поставки на русском языке';

comment on column contract_goszakup.payments_terms_kz is 'Условия поставки на государственном языке';

comment on column contract_goszakup.is_gu is 'Признак ГУ';

comment on column contract_goszakup.exchange_rate is '?Курс валюты (для валютных договоров)';

comment on column contract_goszakup.system_id is 'ИД системы';

comment on column contract_goszakup.index_date is 'Дата индексации';

comment on column contract_goszakup.fakt_exec_date is 'Фактическая дата исполнения';

comment on column contract_goszakup.relevance is 'Локально - Релевантность записи';

create unique index contract_goszakup_id_uindex
    on contract_goszakup (id);


--lot
create table lot_goszakup
(
    id                   integer                             not null
        constraint lot_goszakup_pk
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

comment on table lot_goszakup is 'Реестр лотов
https://ows.goszakup.gov.kz/v3/lots';

comment on column lot_goszakup.id is 'ИД лота';

comment on column lot_goszakup.lot_number is 'Номер лота';

comment on column lot_goszakup.ref_lot_status_id is 'Статус лота';

comment on column lot_goszakup.last_update_date is 'Дата последнего изменения';

comment on column lot_goszakup.union_lots is 'Признак объединенного лота';

comment on column lot_goszakup.count is 'Общее количество';

comment on column lot_goszakup.amount is 'Общая сумма';

comment on column lot_goszakup.name_ru is 'Наименование на русском языке';

comment on column lot_goszakup.name_kz is 'Наименование на государственном языке';

comment on column lot_goszakup.description_ru is 'Детальное описание на русском языке';

comment on column lot_goszakup.description_kz is 'Детальное описание на государственном языке';

comment on column lot_goszakup.customer_id is 'Идентификатор заказчика';

comment on column lot_goszakup.customer_bin is 'БИН заказчика';

comment on column lot_goszakup.trd_buy_number_anno is 'Номер объявления';

comment on column lot_goszakup.trd_buy_id is 'Уникальный идентификатор объявления';

comment on column lot_goszakup.dumping is '?Признак демпинга';

comment on column lot_goszakup.dumping_lot_price is '?Сумма для расчета демпинга';

comment on column lot_goszakup.psd_sign is 'Признак работы. 1-работа с ТЭО/ПСД, 2-работа на разработку ТЭО/ПСД';

comment on column lot_goszakup.consulting_services is 'Признак Консультационная услуга';

comment on column lot_goszakup.is_light_industry is 'Закупка легкой и мебельной промышленности';

comment on column lot_goszakup.is_construction_work is 'Закупка с признаком СМР';

comment on column lot_goszakup.disable_person_id is 'Признак - Закупка среди организаций инвалидов';

comment on column lot_goszakup.customer_name_kz is 'Наименование заказчика на государственном языке';

comment on column lot_goszakup.customer_name_ru is 'Наименование заказчика на русском языке';

comment on column lot_goszakup.ref_trade_methods_id is 'ИД Способа закупки';

comment on column lot_goszakup.index_date is 'Дата индексации';

comment on column lot_goszakup.system_id is 'Уникальный идентификатор системы';

comment on column lot_goszakup.single_org_sign is 'Закупки Единого организатора КГЗ МФ РК';

comment on column lot_goszakup.relevance is 'Локально - Релевантность записи';

create unique index lot_goszakup_id_uindex
    on lot_goszakup (id);


--participant
create table participant_goszakup
(
    pid                      integer                             not null
        constraint participant_goszakup_pk
            primary key,
    bin                      bigint,
    iin                      bigint,
    inn                      text,
    unp                      text,
    regdate                  timestamp,
    crdate                   date,
    index_date               date,
    number_reg               text,
    series                   text,
    name_ru                  text,
    name_kz                  text,
    full_name_ru             text,
    full_name_kz             text,
    country_code             integer,
    customer                 boolean,
    organizer                boolean,
    mark_national_company    boolean,
    ref_kopf_code            text,
    mark_assoc_with_disab    boolean,
    system_id                integer,
    supplier                 boolean,
    krp_code                 integer,
    oked_list                integer,
    kse_code                 integer,
    mark_world_company       boolean,
    mark_state_monopoly      boolean,
    mark_natural_monopoly    boolean,
    mark_patronymic_producer boolean,
    mark_patronymic_supplier boolean,
    mark_small_employer      boolean,
    is_single_org            boolean,
    email                    text,
    phone                    text,
    website                  text,
    last_update_date         timestamp,
    qvazi                    boolean,
    year                     date,
    mark_resident            boolean,
    type_supplier            smallint,
    relevance                timestamp default CURRENT_TIMESTAMP not null
);

comment on table participant_goszakup is 'Реестр участников: Полный список
https://ows.goszakup.gov.kz/v3/subject/all';

comment on column participant_goszakup.pid is 'ID участника';

comment on column participant_goszakup.bin is 'БИН';

comment on column participant_goszakup.iin is 'ИИН';

comment on column participant_goszakup.inn is 'ИНН';

comment on column participant_goszakup.unp is 'УНП';

comment on column participant_goszakup.regdate is 'Дата свидетельства о государственной регистрации';

comment on column participant_goszakup.crdate is 'Дата регистрации';

comment on column participant_goszakup.index_date is 'Дата индексации';

comment on column participant_goszakup.number_reg is 'Номер свидетельства о государственной регистрации';

comment on column participant_goszakup.series is 'Серия свидетельства (для ИП)';

comment on column participant_goszakup.name_ru is 'Наименование на русском языке';

comment on column participant_goszakup.name_kz is 'Наименование на казахском языке';

comment on column participant_goszakup.full_name_ru is 'Полное наименование на русском языке';

comment on column participant_goszakup.full_name_kz is 'Полное наименование на казахском языке';

comment on column participant_goszakup.country_code is 'Страна по ЭЦП';

comment on column participant_goszakup.customer is 'Флаг Заказчик (1 - да, 0 - Нет)';

comment on column participant_goszakup.organizer is 'Флаг Организатор (1 - да, 0 - Нет)';

comment on column participant_goszakup.mark_national_company is 'Флаг Национальная компания (1 - да, 0 - Нет)';

comment on column participant_goszakup.ref_kopf_code is 'Код КОПФ';

comment on column participant_goszakup.mark_assoc_with_disab is 'Флаг Объединение инвалидов (1 - да, 0 - Нет)';

comment on column participant_goszakup.system_id is 'ИД Системы';

comment on column participant_goszakup.supplier is 'Флаг Поставщик (1 - да, 0 - Нет)';

comment on column participant_goszakup.krp_code is '?Размерность предприятия (КРП)';

comment on column participant_goszakup.oked_list is 'ОКЭД';

comment on column participant_goszakup.kse_code is 'Код сектора экономики';

comment on column participant_goszakup.mark_world_company is 'Флаг Международная организация (1 - да, 0 - Нет)';

comment on column participant_goszakup.mark_state_monopoly is 'Флаг Субъект государственной монополии (1 - да, 0 - Нет)';

comment on column participant_goszakup.mark_natural_monopoly is 'Флаг Субъект естественной монополии (1 - да, 0 - Нет)';

comment on column participant_goszakup.mark_patronymic_producer is 'Флаг Отечественный товаропроизводитель (1 - да, 0 - Нет)';

comment on column participant_goszakup.mark_patronymic_supplier is 'Флаг Отечественный поставщик (1 - да, 0 - Нет)';

comment on column participant_goszakup.mark_small_employer is 'Флаг Субъект малого предпринимательства (СМП) (1 - да, 0 - Нет)';

comment on column participant_goszakup.is_single_org is 'Флаг Единый организатор (1 - да, 0 - Нет)';

comment on column participant_goszakup.email is 'E-Mail';

comment on column participant_goszakup.phone is 'Телефон';

comment on column participant_goszakup.website is 'Web сайт';

comment on column participant_goszakup.last_update_date is 'Дата последнего редактирования';

comment on column participant_goszakup.qvazi is 'Флаг Квазисектора';

comment on column participant_goszakup.year is 'Год регистрации';

comment on column participant_goszakup.mark_resident is 'Флаг резидента';

comment on column participant_goszakup.type_supplier is 'Тип поставщика (1 - юридическое лицо, 2 - физическое лицо, 3 - ИП)';

comment on column participant_goszakup.relevance is 'Локально - Релевантность записи';

create unique index participant_goszakup_pid_uindex
    on participant_goszakup (pid);


--unscrupulous
create table unscrupulous_goszakup
(
    pid              integer                             not null
        constraint unscrupulous_goszakup_pk
            primary key,
    supplier_biin    bigint,
    supplier_innunp  text,
    supplier_name_ru text,
    supplier_name_kz text,
    index_date       timestamp,
    system_id        integer,
    relevance        timestamp default CURRENT_TIMESTAMP not null
);

comment on table unscrupulous_goszakup is 'Реестр недобросовестных поставщиков
https://ows.goszakup.gov.kz/v3/rnu';

comment on column unscrupulous_goszakup.pid is 'ID участника';

comment on column unscrupulous_goszakup.supplier_biin is 'БИН/ИИН Участника';

comment on column unscrupulous_goszakup.supplier_innunp is 'ИНН/УНП Участника';

comment on column unscrupulous_goszakup.supplier_name_ru is 'Наименование участника на русском языке';

comment on column unscrupulous_goszakup.supplier_name_kz is 'Наименование участника на казахском языке';

comment on column unscrupulous_goszakup.index_date is 'Дата индексации объекта';

comment on column unscrupulous_goszakup.system_id is 'Идентификатор системы';

comment on column unscrupulous_goszakup.relevance is 'Локально - Релевантность записи';

create unique index unscrupulous_goszakup_pid_uindex
    on unscrupulous_goszakup (pid);

--director
create table director_goszakup
(
    id        serial                              not null
        constraint head_pk
            primary key,
    bin       bigint                              not null,
    iin       bigint,
    rnn       bigint,
    fullname  text,
    relevance timestamp default CURRENT_TIMESTAMP not null
);

create unique index head_id_uindex
    on director_goszakup (id);

--rnu
create table rnu_reference_goszakup
(
    id                    integer                             not null
        constraint rnu_reference_goszakup_pk
            primary key,
    pid                   integer,
    customer_biin         bigint                              not null,
    customer_name_ru      text,
    customer_name_kz      text,
    supplier_biin         bigint                              not null,
    supplier_name_ru      text,
    supplier_name_kz      text,
    supplier_innunp       text,
    supplier_head_name_kz text,
    supplier_head_name_ru text,
    supplier_head_biin    bigint,
    court_decision        text,
    court_decision_date   timestamp,
    start_date            timestamp,
    end_date              timestamp,
    ref_reason_id         integer,
    index_date            timestamp,
    system_id             smallint,
    relevance             timestamp default CURRENT_TIMESTAMP not null
);

create unique index rnu_reference_goszakup_id_uindex
    on rnu_reference_goszakup (id);

--plan
create table plan_goszakup
(
    id                           bigint not null
        constraint plan_goszakup_pk
            primary key,
    plan_act_id                  bigint,
    plan_act_number              text,
    ref_plan_status_id           integer,
    plan_fin_year                integer,
    plan_preliminary             integer,
    rootrecord_id                bigint,
    sys_subjects_id              bigint,
    subject_biin                 bigint,
    name_ru                      text,
    name_kz                      text,
    ref_trade_methods_id         integer,
    ref_units_code               integer,
    count                        double precision,
    price                        double precision,
    amount                       double precision,
    ref_months_id                integer,
    ref_pln_point_status_id      integer,
    pln_point_year               integer,
    ref_subject_types_id         integer,
    ref_enstru_code              text,
    ref_finsource_id             integer,
    ref_abp_code                 integer,
    date_approved                timestamp,
    is_qvazi                     integer,
    date_create                  timestamp,
    timestamp                    timestamp,
    system_id                    integer,
    ref_point_type_id            integer,
    desc_ru                      text,
    desc_kz                      text,
    extra_desc_ru                text,
    extra_desc_kz                text,
    sum_1                        bigint,
    sum_2                        bigint,
    sum_3                        bigint,
    supply_date_ru               text,
    prepayment                   double precision,
    ref_justification_id         integer,
    ref_amendment_agreem_type_id integer,
    ref_amendm_agreem_justif_id  integer,
    contract_prev_point_id       integer,
    disable_person_id            integer,
    transfer_sys_subjects_id     integer,
    transfer_type                integer,
    ref_budget_type_id           integer,
    subject_name_kz              text,
    subject_name_ru              text,
    relevance                    timestamp default CURRENT_TIMESTAMP
);

create unique index plan_goszakup_id_uindex
    on plan_goszakup (id);


--ref_unit
create table ref_unit_goszakup
(
    id         serial not null,
    name_ru    text,
    name_kz    text,
    code       integer,
    code2      text,
    alpha_code text
);

create unique index ref_unit_goszakup_id_uindex
    on ref_unit_goszakup (id);


--ref_buy_status
create table ref_buy_status_goszakup
(
    id      serial not null,
    name_ru text,
    name_kz text,
    code    text
);

create unique index ref_buy_status_goszakup_id_uindex
    on ref_buy_status_goszakup (id);


--ref_lot_status
create table ref_lot_status_goszakup
(
    id      serial not null,
    name_ru text,
    name_kz text,
    code    text
);

create unique index ref_lots_status_goszakup_id_uindex
    on ref_lot_status_goszakup (id);


--ref_trade_method
create table ref_trade_method_goszakup
(
    id          serial not null,
    name_ru     text,
    name_kz     text,
    symbol_code text,
    code        integer,
    is_active   boolean,
    type        integer,
    f1          integer,
    ord         integer,
    f2          integer
);

create unique index ref_trade_method_goszakup_id_uindex
    on ref_trade_method_goszakup (id);