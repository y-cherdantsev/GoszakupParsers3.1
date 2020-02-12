create table all_participants_goszakup
(
    pid                      integer                             not null
        constraint all_participants_goszakup_pk
            primary key,
    bin                      bigint,
    iin                      bigint,
    inn                      text,
    unp                      text,
    regdate                  timestamp,
    crdate                   integer,
    index_date               text,
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
    year                     integer,
    mark_resident            boolean,
    type_supplier            smallint,
    relevance                timestamp default CURRENT_TIMESTAMP not null
);

comment on table all_participants_goszakup is 'Реестр участников: Полный список
https://ows.goszakup.gov.kz/v3/subject/all';

comment on column all_participants_goszakup.pid is 'ID участника';

comment on column all_participants_goszakup.bin is 'БИН';

comment on column all_participants_goszakup.iin is 'ИИН';

comment on column all_participants_goszakup.inn is 'ИНН';

comment on column all_participants_goszakup.unp is 'УНП';

comment on column all_participants_goszakup.regdate is 'Дата свидетельства о государственной регистрации';

comment on column all_participants_goszakup.crdate is 'Дата регистрации';

comment on column all_participants_goszakup.index_date is 'Дата индексации';

comment on column all_participants_goszakup.number_reg is 'Номер свидетельства о государственной регистрации';

comment on column all_participants_goszakup.series is 'Серия свидетельства (для ИП)';

comment on column all_participants_goszakup.name_ru is 'Наименование на русском языке';

comment on column all_participants_goszakup.name_kz is 'Наименование на казахском языке';

comment on column all_participants_goszakup.full_name_ru is 'Полное наименование на русском языке';

comment on column all_participants_goszakup.full_name_kz is 'Полное наименование на казахском языке';

comment on column all_participants_goszakup.country_code is 'Страна по ЭЦП';

comment on column all_participants_goszakup.customer is 'Флаг Заказчик (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.organizer is 'Флаг Организатор (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.mark_national_company is 'Флаг Национальная компания (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.ref_kopf_code is 'Код КОПФ';

comment on column all_participants_goszakup.mark_assoc_with_disab is 'Флаг Объединение инвалидов (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.system_id is 'ИД Системы';

comment on column all_participants_goszakup.supplier is 'Флаг Поставщик (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.krp_code is '?Размерность предприятия (КРП)';

comment on column all_participants_goszakup.oked_list is 'ОКЭД';

comment on column all_participants_goszakup.kse_code is 'Код сектора экономики';

comment on column all_participants_goszakup.mark_world_company is 'Флаг Международная организация (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.mark_state_monopoly is 'Флаг Субъект государственной монополии (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.mark_natural_monopoly is 'Флаг Субъект естественной монополии (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.mark_patronymic_producer is 'Флаг Отечественный товаропроизводитель (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.mark_patronymic_supplier is 'Флаг Отечественный поставщик (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.mark_small_employer is 'Флаг Субъект малого предпринимательства (СМП) (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.is_single_org is 'Флаг Единый организатор (1 - да, 0 - Нет)';

comment on column all_participants_goszakup.email is 'E-Mail';

comment on column all_participants_goszakup.phone is 'Телефон';

comment on column all_participants_goszakup.website is 'Web сайт';

comment on column all_participants_goszakup.last_update_date is 'Дата последнего редактирования';

comment on column all_participants_goszakup.qvazi is 'Флаг Квазисектора';

comment on column all_participants_goszakup.year is 'Год регистрации';

comment on column all_participants_goszakup.mark_resident is 'Флаг резидента';

comment on column all_participants_goszakup.type_supplier is 'Тип поставщика (1 - юридическое лицо, 2 - физическое лицо, 3 - ИП)';

comment on column all_participants_goszakup.relevance is 'Локально - Релевантность записи';

create unique index all_participants_goszakup_pid_uindex
    on all_participants_goszakup (pid);