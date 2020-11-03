--announcement
create table announcement_goszakup
(
    id               bigserial not null
        constraint announcement_goszakup_new_pk
            primary key,
    number_anno      text,
    count_lots       integer,
    total_sum        double precision,
    start_date       timestamp,
    end_date         timestamp,
    publish_date     timestamp,
    type_trade       text,
    trade_method     text,
    subject_type     text,
    buy_status       text,
    organizator_biin bigint,
    name_ru          text,
    count            double precision
);

create unique index announcement_goszakup_new_id_uindex
    on announcement_goszakup (id);


--lot
create table lot_goszakup
(
    id             bigserial not null
        constraint lot_goszakup_new_pk
            primary key,
    anno_id        bigint
        constraint lot_goszakup_new_announcement_goszakup_new_id_fk
            references announcement_goszakup
            on update cascade on delete cascade,
    lot_number     text,
    name_ru        text,
    description_ru text,
    customer_bin   bigint,
    amount         double precision,
    lot_status     text,
    tru_code       text,
    supply_date_ru text,
    units          text,
    delivery_place text,
    count          double precision
);

create unique index lot_goszakup_new_id_uindex
    on lot_goszakup (id);


--contract
create table contract_goszakup
(
    id                    bigserial not null
        constraint contract_goszakup_new_pk
            primary key,
    contract_number       text,
    contract_number_sys   text,
    supplier_biin         bigint,
    customer_bin          bigint,
    supplier_iik          text,
    customer_iik          text,
    fin_year              integer,
    sign_date             timestamp,
    ec_end_date           timestamp,
    description_ru        text,
    contract_sum_wnds     double precision,
    fakt_sum_wnds         double precision,
    supplier_bank_name_ru text,
    customer_bank_name_ru text,
    supplier_bik          text,
    customer_bik          text,
    agr_form              text,
    year_type             text,
    trade_method          text,
    status                text,
    type                  text,
    announcement_number   text,
    create_date           timestamp,
    doc_link              text,
    doc_name              text
);

create unique index contract_goszakup_new_id_uindex
    on contract_goszakup (id);


--contract-unit
create table contract_unit_goszakup
(
    id               bigserial not null
        constraint contract_units_pk
            primary key,
    item_price       double precision,
    item_price_wnds  double precision,
    quantity         double precision,
    total_sum        double precision,
    total_sum_wnds   double precision,
    contract_id      bigint
        constraint contract_unit_goszakup_contract_goszakup_new_id_fk
            references contract_goszakup
            on update cascade on delete cascade,
    source_unique_id bigint
);


--contract-plan
create table plan_goszakup
(
    id                bigserial not null
        constraint plan_goszakup_new_pk
            primary key,
    act_number        text,
    fin_year          integer,
    date_approved     timestamp,
    status            text,
    method            text,
    name              text,
    description       text,
    measure           text,
    count             double precision,
    price             double precision,
    amount            double precision,
    month_id          integer,
    is_qvazi          boolean,
    tru_code          text,
    date_create       timestamp,
    extra_description text,
    supply_date       text,
    prepayment        double precision,
    subject_biin      bigint,
    contract_unit_id  bigint
        constraint plan_goszakup_new_contract_unit_goszakup_id_fk
            references contract_unit_goszakup
            on update cascade on delete cascade,
    source_unique_id  bigint
);

create unique index plan_goszakup_new_id_uindex
    on plan_goszakup (id);


--participant
create table participant_goszakup
(
    pid integer not null
        constraint participant_goszakup_pk
            primary key,
    bin bigint,
    iin bigint,
    inn text,
    unp text,
    regdate timestamp,
    crdate date,
    index_date date,
    number_reg text,
    series text,
    name_ru text,
    name_kz text,
    full_name_ru text,
    full_name_kz text,
    country_code integer,
    customer boolean,
    organizer boolean,
    mark_national_company boolean,
    ref_kopf_code text,
    mark_assoc_with_disab boolean,
    system_id integer,
    supplier boolean,
    krp_code integer,
    oked_list integer,
    kse_code integer,
    mark_world_company boolean,
    mark_state_monopoly boolean,
    mark_natural_monopoly boolean,
    mark_patronymic_producer boolean,
    mark_patronymic_supplier boolean,
    mark_small_employer boolean,
    is_single_org boolean,
    email text,
    phone text,
    website text,
    last_update_date timestamp,
    qvazi boolean,
    year date,
    mark_resident boolean,
    type_supplier smallint,
    relevance timestamp default CURRENT_TIMESTAMP not null
);

create unique index participant_goszakup_pid_uindex
    on participant_goszakup (pid);


--unscrupulous
create table unscrupulous_goszakup
(
    pid integer not null
        constraint unscrupulous_goszakup_pk
            primary key,
    supplier_biin bigint,
    supplier_innunp text,
    supplier_name_ru text,
    supplier_name_kz text,
    index_date timestamp,
    system_id integer,
    relevance timestamp default CURRENT_TIMESTAMP not null
);

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


--tender_document_goszakup
create table tender_document_goszakup
(
    id        serial not null
        constraint tender_document_goszakup_pk
            primary key,
    identity  text   not null,
    number    text   not null,
    type      text   not null,
    title     text   not null,
    link      text   not null,
    relevance timestamp default now()
);

create unique index tender_document_goszakup_id_uindex
    on tender_document_goszakup (id);
