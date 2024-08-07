﻿using System.ComponentModel;

namespace AsiecSchedule.Data.Asiec
{
    [DefaultValue(None)]
    public enum RequestType
    {
        None,
        GroupId,
        TeacherId,
        ClassroomId
    }

    public class AsiecData
    {

        public static Dictionary<string, string> GroupIDs { get; } = new Dictionary<string, string>()
        {
            { "11Б211", "98dc3d20-04aa-11ec-8a63-00155d879809" },
            { "11Б221", "3afb102a-1ea1-11ed-abe0-00155d879809" },
            { "11Б231", "ba556017-3d63-11ee-9626-00155d879809" },
            { "11Бд223", "3afb1029-1ea1-11ed-abe0-00155d879809" },
            { "11Бд232", "ba556019-3d63-11ee-9626-00155d879809" },
            { "11ЗБ211", "32b17c96-048b-11ec-8a63-00155d879809" },
            { "11ЗБ221", "3afb103f-1ea1-11ed-abe0-00155d879809" },
            { "11ЗБ231", "ba556021-3d63-11ee-9626-00155d879809" },
            { "11ЗЕМ232", "ba55601a-3d63-11ee-9626-00155d879809" },
            { "11ЗЕМ233", "ba55601b-3d63-11ee-9626-00155d879809" },
            { "11ЗИО222", "3afb1033-1ea1-11ed-abe0-00155d879809" },
            { "11ЗПд211", "32b17c98-048b-11ec-8a63-00155d879809" },
            { "11ЗПд221", "3afb1040-1ea1-11ed-abe0-00155d879809" },
            { "11ЗПд222", "3afb1041-1ea1-11ed-abe0-00155d879809" },
            { "11ЗПд231", "ba556022-3d63-11ee-9626-00155d879809" },
            { "11ЗТ211", "32b17c97-048b-11ec-8a63-00155d879809" },
            { "11ЗТ221", "3afb1042-1ea1-11ed-abe0-00155d879809" },
            { "11ИСиП212", "013b4e24-04a7-11ec-8a63-00155d879809" },
            { "11ИСиП232", "ba556020-3d63-11ee-9626-00155d879809" },
            { "11ОИБ222", "3afb1036-1ea1-11ed-abe0-00155d879809" },
            { "11ОИБ232", "ba55601c-3d63-11ee-9626-00155d879809" },
            { "11Пд224", "3afb103d-1ea1-11ed-abe0-00155d879809" },
            { "11Пд225", "3afb103e-1ea1-11ed-abe0-00155d879809" },
            { "11ПД235", "ba55601e-3d63-11ee-9626-00155d879809" },
            { "11ПД236", "ba55601f-3d63-11ee-9626-00155d879809" },
            { "11ПСО222", "3afb1031-1ea1-11ed-abe0-00155d879809" },
            { "11Сд221", "3afb102b-1ea1-11ed-abe0-00155d879809" },
            { "11ССА211", "8e35b072-04b0-11ec-8a63-00155d879809" },
            { "11ССА221", "3afb1037-1ea1-11ed-abe0-00155d879809" },
            { "11ССА231", "ba55601d-3d63-11ee-9626-00155d879809" },
            { "11Т232", "ba556018-3d63-11ee-9626-00155d879809" },
            { "11ТОР212", "1f148242-04a6-11ec-8a63-00155d879809" },
            { "11Ф222", "3afb102d-1ea1-11ed-abe0-00155d879809" },
            { "11Эк211", "013b4e06-04a7-11ec-8a63-00155d879809" },
            { "11Эк221", "3afb1034-1ea1-11ed-abe0-00155d879809" },
            { "9Бд211", "1d3f340d-048f-11ec-8a63-00155d879809" },
            { "9Бд221", "3afb1027-1ea1-11ed-abe0-00155d879809" },
            { "9Бд222", "3afb1028-1ea1-11ed-abe0-00155d879809" },
            { "9БД231", "71d4f03f-3cc0-11ee-9626-00155d879809" },
            { "9ЗЕМ231", "71d4f044-3cc0-11ee-9626-00155d879809" },
            { "9ЗИО211", "1d3f341d-048f-11ec-8a63-00155d879809" },
            { "9ЗИО221", "3afb1032-1ea1-11ed-abe0-00155d879809" },
            { "9ИСиП201", "601e44a7-e5bb-11ea-b7db-00155d879809" },
            { "9ИСиП211", "ed6590df-0491-11ec-8a63-00155d879809" },
            { "9ИСиП221", "3afb1038-1ea1-11ed-abe0-00155d879809" },
            { "9ИСиП222", "3afb1039-1ea1-11ed-abe0-00155d879809" },
            { "9ИСиП231", "71d4f046-3cc0-11ee-9626-00155d879809" },
            { "9ОИБ201", "601e44a6-e5bb-11ea-b7db-00155d879809" },
            { "9ОИБ211", "ed6590fa-0491-11ec-8a63-00155d879809" },
            { "9ОИБ221", "3afb1035-1ea1-11ed-abe0-00155d879809" },
            { "9ОИБ231", "71d4f045-3cc0-11ee-9626-00155d879809" },
            { "9Пд211", "7f2d3e07-0493-11ec-8a63-00155d879809" },
            { "9Пд212", "7f2d3e08-0493-11ec-8a63-00155d879809" },
            { "9Пд213", "c5b70cf2-0494-11ec-8a63-00155d879809" },
            { "9Пд221", "3afb103a-1ea1-11ed-abe0-00155d879809" },
            { "9Пд222", "3afb103b-1ea1-11ed-abe0-00155d879809" },
            { "9Пд223", "3afb103c-1ea1-11ed-abe0-00155d879809" },
            { "9ПД231", "71d4f047-3cc0-11ee-9626-00155d879809" },
            { "9ПД232", "71d4f04e-3cc0-11ee-9626-00155d879809" },
            { "9ПД233", "71d4f04f-3cc0-11ee-9626-00155d879809" },
            { "9ПД234", "71d4f050-3cc0-11ee-9626-00155d879809" },
            { "9ПСО211", "110160a4-0487-11ec-8a63-00155d879809" },
            { "9ПСО221", "3afb1030-1ea1-11ed-abe0-00155d879809" },
            { "9ПСО231", "71d4f043-3cc0-11ee-9626-00155d879809" },
            { "9Сд211", "db11bd21-0492-11ec-8a63-00155d879809" },
            { "9СД231", "71d4f040-3cc0-11ee-9626-00155d879809" },
            { "9Т211", "7f2d3de1-0493-11ec-8a63-00155d879809" },
            { "9Т221", "3afb102e-1ea1-11ed-abe0-00155d879809" },
            { "9Т231", "71d4f042-3cc0-11ee-9626-00155d879809" },
            { "9ТОР201", "7b4062d5-e377-11ea-b7db-00155d879809" },
            { "9ТОР211", "db11bcfb-0492-11ec-8a63-00155d879809" },
            { "9ТОР221", "3afb102f-1ea1-11ed-abe0-00155d879809" },
            { "9Ф211", "7f2d3e06-0493-11ec-8a63-00155d879809" },
            { "9Ф221", "3afb102c-1ea1-11ed-abe0-00155d879809" },
            { "9Ф231", "71d4f041-3cc0-11ee-9626-00155d879809" },
            { "Бд БЖД юноши", "14df0b97-11db-11ec-ad2e-00155d879809" },
            { "ПСО БЖД юноши", "14df0b96-11db-11ec-ad2e-00155d879809" }
        };

        public static Dictionary<string, string> TeacherIDs { get; } = new Dictionary<string, string>
        {
            { "Акимкина Ольга Николаевна", "fd3fec13-cd78-11eb-a600-00155d879809" },
            { "Амелькина Алина Алексеевна", "a7f8fd2d-a332-11ec-bad0-00155d879809" },
            { "Американцева Алена Сергеевна", "6a439cf5-9d83-11ee-a325-c6fea011313f" },
            { "Анисимова Татьяна Анатольевна", "be5f86c3-fb0b-11ee-b397-c6fea011313f" },
            { "Антонова Марина Владимировна", "54a5850c-3604-11eb-a90d-00155d879809" },
            { "Асеев Алексей Алексеевич", "b2f4c62f-4e1a-11ee-8e80-00155d879809" },
            { "Астафьева Елена Александровна", "bc48d57e-06e8-11ec-ad2e-00155d879809" },
            { "Бадосова Елена Викторовна", "3006bc15-360d-11eb-a90d-00155d879809" },
            { "Бакеев Тимур Камильевич", "01c22796-4df7-11ee-8e80-00155d879809" },
            { "Бардаш Андрей Иванович", "17f0bd11-273b-11ed-abe0-00155d879809" },
            { "Барсукова Татьяна Геннадьевна", "54a5850b-3604-11eb-a90d-00155d879809" },
            { "Барыбина Наталья Валерьевна", "3006bc21-360d-11eb-a90d-00155d879809" },
            { "Баскакова Надежда Олеговна", "8c99b355-175b-11ec-ad2e-00155d879809" },
            { "Безбородова Татьяна Александровна", "54a5850d-3604-11eb-a90d-00155d879809" },
            { "Белоносова Татьяна Сергеевна", "690f80b9-27d5-11ec-8ea5-00155d879809" },
            { "Бельчиков Сергей Андреевич", "d0b89977-45e7-11ed-886f-00155d879809" },
            { "Белякова Наталья Семёновна", "54a5850e-3604-11eb-a90d-00155d879809" },
            { "Бобров Владимир Александрович", "54a5850f-3604-11eb-a90d-00155d879809" },
            { "Божок Сергей Сергеевич", "3006bc22-360d-11eb-a90d-00155d879809" },
            { "Брехов Денис Вадимович", "54a58520-3604-11eb-a90d-00155d879809" },
            { "Бровко Екатерина Александровна", "0b44ef9d-8200-11ee-b4f8-c6fea011313f" },
            { "Вакансия (10)", "a4c10820-3950-11ed-abe0-00155d879809" },
            { "Вакансия (11)", "f80d6ad7-3e09-11ed-abe0-00155d879809" },
            { "Вакансия (12)", "f80d6ad9-3e09-11ed-abe0-00155d879809" },
            { "Вакансия (14)", "970d0e4d-43c5-11ed-886f-00155d879809" },
            { "Вакансия (15)", "b939c60a-50d7-11ed-99b8-00155d879809" },
            { "Вакансия (3)", "5b503cdc-349b-11ed-abe0-00155d879809" },
            { "Вакансия (4)", "5b503cde-349b-11ed-abe0-00155d879809" },
            { "Вакансия (5)", "5b503ce0-349b-11ed-abe0-00155d879809" },
            { "Вакансия (6)", "5b503ce2-349b-11ed-abe0-00155d879809" },
            { "Вакансия (7)", "5b503ce4-349b-11ed-abe0-00155d879809" },
            { "Вакансия (8)", "085be89a-3887-11ed-abe0-00155d879809" },
            { "Вакансия (9)", "a4c1081e-3950-11ed-abe0-00155d879809" },
            { "Валькова Татьяна Васильевна", "d9b537bf-2fc9-11ec-a1eb-00155d879809" },
            { "Васильева Алла Михайловна", "e9aee4ec-28d7-11ed-abe0-00155d879809" },
            { "Ведяшкина Александра Павловна", "d98bf9c5-e1a8-11ee-b397-c6fea011313f" },
            { "Ветрова Любовь Владимировна", "5328980b-6d72-11ee-9bfc-c6fea011313f" },
            { "Винтер Елена Анатольевна", "6a439cf8-9d83-11ee-a325-c6fea011313f" },
            { "Владимирцев Владислав Владимирович", "92897356-7e08-11ee-b4f8-c6fea011313f" },
            { "Власов Александр Александрович", "e0c65997-60bf-11ee-b898-00155d879809" },
            { "Волвенкина Светлана Сергеевна", "d2ab38d6-2f96-11eb-86b3-00155d879809" },
            { "Воронцова Светлана Анатольевна", "dacf76ee-b9dd-11eb-8ae9-00155d879809" },
            { "Гайворонский Александр Александрович", "c7728301-581a-11ee-8e80-00155d879809" },
            { "Гайдукова Кристина Михайловна", "8333d82b-633a-11ee-9bfc-c6fea011313f" },
            { "Говорова  Оксана  Юрьевна", "54a58532-3604-11eb-a90d-00155d879809" },
            { "Говорова Анастасия Сергеевна", "fd3fec0b-cd78-11eb-a600-00155d879809" },
            { "Голик Ирина Петровна", "f9ee76dd-a4b2-11ea-98c2-00155d879809" },
            { "Гольцева Наталья Ивановна", "99f117cc-358b-11ed-abe0-00155d879809" },
            { "Гомляков Сергей  Владимирович", "3006bc16-360d-11eb-a90d-00155d879809" },
            { "Грибов Александр Михайлович", "644ce940-b69a-11ee-a940-c6fea011313f" },
            { "Грозова Ольга Александровна", "f9ee76da-a4b2-11ea-98c2-00155d879809" },
            { "Дашкова Анна Валерьевна", "8901155c-284a-11ed-abe0-00155d879809" },
            { "Драга Мария Александровна", "d2ab38d8-2f96-11eb-86b3-00155d879809" },
            { "Дружинин Александр Викторович", "54a58521-3604-11eb-a90d-00155d879809" },
            { "Дудников  Борис  Борисович", "54a58511-3604-11eb-a90d-00155d879809" },
            { "Егорова Жанна Рафаильевна", "096f3500-0a0c-11ec-ad2e-00155d879809" },
            { "Еремкина Наталья Юрьевна", "25d7a4ef-479c-11ee-9626-00155d879809" },
            { "Жуган Наталья Николаевна", "bc48d580-06e8-11ec-ad2e-00155d879809" },
            { "Завьялова Людмила Алексеевна", "54a58522-3604-11eb-a90d-00155d879809" },
            { "Захарова Наталья Александровна", "54a58523-3604-11eb-a90d-00155d879809" },
            { "Зенкина Татьяна Евгеньевна", "b6af2688-b370-11ee-a940-c6fea011313f" },
            { "Зенкина Татьяна Евгеньевна 2", "644ce93d-b69a-11ee-a940-c6fea011313f" },
            { "Зимин Дмитрий Викторович", "3268ca0f-299c-11ed-abe0-00155d879809" },
            { "Иванова Лариса Викторовна", "54a58513-3604-11eb-a90d-00155d879809" },
            { "Иващенко Евгения Сергеевна", "54a58533-3604-11eb-a90d-00155d879809" },
            { "Исаева Ольга Сергеевна", "63461bef-a4a5-11ea-98c2-00155d879809" },
            { "Калуцкий Александр Александрович", "54a58514-3604-11eb-a90d-00155d879809" },
            { "Квасов Борис Владимирович", "54a58525-3604-11eb-a90d-00155d879809" },
            { "Киржаева Людмила Алексеевна", "54a58534-3604-11eb-a90d-00155d879809" },
            { "Кисиль Марина Станиславовна ", "3006bc14-360d-11eb-a90d-00155d879809" },
            { "Ковалева Маргарита Ивановна", "bc48d58a-06e8-11ec-ad2e-00155d879809" },
            { "Кожухова Анастасия Юрьевна", "c9b03b52-1697-11ec-ad2e-00155d879809" },
            { "Козлобаев Дмитрий Николаевич", "b4571a78-3ade-11ed-abe0-00155d879809" },
            { "Козлова Галина Юрьевна", "edbb5d74-4868-11ee-9626-00155d879809" },
            { "Колова Светлана Николаевна", "3006bc18-360d-11eb-a90d-00155d879809" },
            { "Коломеец Анастасия Максимовна", "3b947e5e-5d06-11ee-b898-00155d879809" },
            { "Кораблина Светлана Олеговна", "54a58515-3604-11eb-a90d-00155d879809" },
            { "Коргун Мария Алексеевна", "25d7a4ed-479c-11ee-9626-00155d879809" },
            { "Котова Наталия Аркадьевна", "d2ab38dc-2f96-11eb-86b3-00155d879809" },
            { "Кох Алёна Владимировна", "4ca390ab-46eb-11ee-9626-00155d879809" },
            { "Кочетыгов Игорь Витальевич", "b8c82c1f-241c-11ed-abe0-00155d879809" },
            { "Краснов Ярослав Владимирович", "e547d7fa-c097-11ee-b35d-c6fea011313f" },
            { "Кудина Наталья Вячеславовна", "f83b4d66-c0b6-11ee-b35d-c6fea011313f" },
            { "Кузьменко Александр Васильевич", "6b537863-9093-11ec-bad0-00155d879809" },
            { "Кузьмин Александр Константинович", "cf861e82-a213-11ed-87c5-00155d879809" },
            { "Курильская Елена Анатольевна", "8c99b349-175b-11ec-ad2e-00155d879809" },
            { "Ларионова Нина Федоровна", "54a58517-3604-11eb-a90d-00155d879809" },
            { "Лебедева Анна Борисовна", "8c99b359-175b-11ec-ad2e-00155d879809" },
            { "Легостаева Анастасия Андреевна", "54a5853f-3604-11eb-a90d-00155d879809" },
            { "Леонова Анна Петровна", "d2ab38d3-2f96-11eb-86b3-00155d879809" },
            { "Лихачёва Наталья Александровна", "54a58536-3604-11eb-a90d-00155d879809" },
            { "Лобада Максим Владимирович", "59e16b3d-41d3-11ec-95fe-00155d879809" },
            { "Логачева Алена Юрьевна", "8bb97e73-4e8f-11ed-8de5-00155d879809" },
            { "Макарова Яна Владимировна", "b6af2686-b370-11ee-a940-c6fea011313f" },
            { "Макарова Яна Владимировна 2", "644ce937-b69a-11ee-a940-c6fea011313f" },
            { "Макашова Юлия Александровна", "6fcab989-6801-11ee-9bfc-c6fea011313f" },
            { "Маланичев Дмитрий Анатольевич", "3006bc19-360d-11eb-a90d-00155d879809" },
            { "Мамчур Евгений Александрович", "4ca390bf-46eb-11ee-9626-00155d879809" },
            { "Медников Алексей Николаевич", "eee72a4e-99cd-11ec-bad0-00155d879809" },
            { "Миронович Екатерина Витальевна", "6736408c-53e8-11eb-b18b-00155d879809" },
            { "Михайлова Тамара Валерьевна", "54a58527-3604-11eb-a90d-00155d879809" },
            { "Назаренко Анастасия Александровна", "3670d30f-46db-11ee-9626-00155d879809" },
            { "Панурина Вера Викторовна", "66946ef5-faf8-11ee-b397-c6fea011313f" },
            { "Паралёва  Инга  Борисовна", "54a58537-3604-11eb-a90d-00155d879809" },
            { "Петрова Антонина Николаевна", "bc48d588-06e8-11ec-ad2e-00155d879809" },
            { "Проскурина Ирина Викторовна", "54a58538-3604-11eb-a90d-00155d879809" },
            { "Раченкова Галина Юрьевна", "3006bc29-360d-11eb-a90d-00155d879809" },
            { "Руппель Андрей Иванович", "edbb5d6e-4868-11ee-9626-00155d879809" },
            { "Руппель Андрей Иванович 2", "bc48d58d-06e8-11ec-ad2e-00155d879809" },
            { "Рыбалко Марина Николаевна", "f9ee76e2-a4b2-11ea-98c2-00155d879809" },
            { "Саенко Галина Владимировна", "3006bc1b-360d-11eb-a90d-00155d879809" },
            { "Сёмина Ольга Анатольевна", "3006bc2a-360d-11eb-a90d-00155d879809" },
            { "Сизова Елена Михайловна", "0830584f-773a-11ec-a8b1-00155d879809" },
            { "Силина Анастасия Евгеньевна", "bc48d58c-06e8-11ec-ad2e-00155d879809" },
            { "Сиротина Ольга Сергеевна", "737a7426-b07f-11ee-9128-c6fea011313f" },
            { "Смирнов Ярослав Андреевич", "3670d310-46db-11ee-9626-00155d879809" },
            { "Смотрикин Василий Анатольевич", "54a58528-3604-11eb-a90d-00155d879809" },
            { "Сородоенко Александра Александровна", "54a58539-3604-11eb-a90d-00155d879809" },
            { "Степаненко Александр Александрович", "47e6f3c7-37a1-11ec-95fe-00155d879809" },
            { "Строителева Марина Сергеевна", "54a58543-3604-11eb-a90d-00155d879809" },
            { "Суворова Анжелика Владимировна", "6789a0fe-ebf4-11ee-b397-c6fea011313f" },
            { "Сулягина Дарья Олеговна", "05adb763-7e05-11ee-b4f8-c6fea011313f" },
            { "Суховеркова Анна Владиславовна", "0db53c7f-0ac9-11ec-ad2e-00155d879809" },
            { "Телеш Никита Сергеевич", "f18fe600-4701-11ee-9626-00155d879809" },
            { "Терентьева Марина Александровна", "3006bc12-360d-11eb-a90d-00155d879809" },
            { "Терленко Екатерина Николаевна", "541eedfa-2f91-11eb-86b3-00155d879809" },
            { "Тимофеева Оксана Владимировна", "54a5853c-3604-11eb-a90d-00155d879809" },
            { "Тишкова Юлия Константиновна", "3006bc1d-360d-11eb-a90d-00155d879809" },
            { "Тогусов Николай Юрьевич", "54a5852a-3604-11eb-a90d-00155d879809" },
            { "Торбеева Кристина Анатольевна", "3bb57b94-af5a-11ee-9128-c6fea011313f" },
            { "Тупикина Алина Юрьевна", "8c99b353-175b-11ec-ad2e-00155d879809" },
            { "Тучина Нина Васильевна", "54a5852b-3604-11eb-a90d-00155d879809" },
            { "Убей-Конь Виктория Викторовна", "54a5852c-3604-11eb-a90d-00155d879809" },
            { "Федосцева Елена Владимировна", "54a5853d-3604-11eb-a90d-00155d879809" },
            { "Фиганова Ольга Николаевна", "ec34f8a7-8500-11ee-b4f8-c6fea011313f" },
            { "Черданцева Ирина Александровна", "14a29722-6662-11ee-9bfc-c6fea011313f" },
            { "Чернышова Татьяна Викторовна", "541eedf8-2f91-11eb-86b3-00155d879809" },
            { "Чеховская Ирина Викторовна", "b3d2e10d-86c7-11eb-8901-00155d879809" },
            { "Чеченева Наталья Ивановна", "3006bc13-360d-11eb-a90d-00155d879809" },
            { "Чирская  Любовь Александровна", "54a5852e-3604-11eb-a90d-00155d879809" },
            { "Шаврова Виктория Анатольевна", "d2ab38da-2f96-11eb-86b3-00155d879809" },
            { "Шампанер Галина Марковна", "54a58530-3604-11eb-a90d-00155d879809" },
            { "Шестакова Надежда Викторовна", "93cccb57-0eb2-11ec-ad2e-00155d879809" },
            { "Штоколова Юлия Антольевна", "096f3514-0a0c-11ec-ad2e-00155d879809" },
            { "Эмм Иван Сергеевич", "cc5e544b-f712-11ee-b397-c6fea011313f" },
            { "Юдакова Анастасия Андреевна", "bc48d592-06e8-11ec-ad2e-00155d879809" },
            { "Юрикова Вероника Владимировна", "5e66cb5c-2fe7-11ed-abe0-00155d879809" },
            { "Юферева Наталья Иннокентьевна", "54a5851c-3604-11eb-a90d-00155d879809" },
            { "Яношко Максим Юрьевич", "b5d2352d-a060-11ed-87c5-00155d879809" }
        };

        public static Dictionary<string, string> ClassroomIDs { get; } = new Dictionary<string, string>()
        {
            { "Корпус 1 / 100", "54a58509-3604-11eb-a90d-00155d879809" },
            { "Корпус 1 / 106", "fb88c0cf-3fcb-11ee-9626-00155d879809" },
            { "Корпус 1 / 109", "3006bc39-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 115", "3006bc34-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 117", "3006bc35-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 209", "3006bc3a-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 213", "1a20a917-093b-11ec-ad2e-00155d879809" },
            { "Корпус 1 / 215", "3006bc38-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 218 А", "3006bc36-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 218 Б", "3006bc37-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 301", "fd22ba5f-35fc-11eb-a90d-00155d879809" },
            { "Корпус 1 / 302", "0b31f95c-2d60-11ec-bb05-00155d879809" },
            { "Корпус 1 / 303", "fd22ba5d-35fc-11eb-a90d-00155d879809" },
            { "Корпус 1 / 304", "fd22ba61-35fc-11eb-a90d-00155d879809" },
            { "Корпус 1 / 305", "54a58504-3604-11eb-a90d-00155d879809" },
            { "Корпус 1 / 307", "54a58505-3604-11eb-a90d-00155d879809" },
            { "Корпус 1 / 308", "54a58507-3604-11eb-a90d-00155d879809" },
            { "Корпус 1 / 309", "3006bc32-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 310", "fd22ba5e-35fc-11eb-a90d-00155d879809" },
            { "Корпус 1 / 401", "54a58508-3604-11eb-a90d-00155d879809" },
            { "Корпус 1 / 402", "fd22ba5b-35fc-11eb-a90d-00155d879809" },
            { "Корпус 1 / 403", "3006bc2e-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 404", "3006bc2f-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 406", "3006bc30-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 407", "3006bc31-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 408", "3006bc33-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / 409", "3006bc3b-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / Актовый", "f63ddd23-0c60-11ec-ad2e-00155d879809" },
            { "Корпус 1 / спорт зал", "3006bc3d-360d-11eb-a90d-00155d879809" },
            { "Корпус 1 / стад", "1a20a918-093b-11ec-ad2e-00155d879809" },
            { "Корпус 1 / Трен Зал ", "fb88c0ce-3fcb-11ee-9626-00155d879809" },
            { "Корпус 2 / ?1", "a4911189-4387-11ed-886f-00155d879809" },
            { "Корпус 2 / ?2", "a6ff18ac-0b8f-11ec-ad2e-00155d879809" },
            { "Корпус 2 / 100", "dacf76f0-b9dd-11eb-8ae9-00155d879809" },
            { "Корпус 2 / 105", "82b7eae1-3e8f-11eb-b18b-00155d879809" },
            { "Корпус 2 / 107", "82b7eaeb-3e8f-11eb-b18b-00155d879809" },
            { "Корпус 2 / 110", "82b7eae5-3e8f-11eb-b18b-00155d879809" },
            { "Корпус 2 / 200", "e851985b-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 202", "1189ac16-3e98-11eb-b18b-00155d879809" },
            { "Корпус 2 / 203", "0db53c6b-0ac9-11ec-ad2e-00155d879809" },
            { "Корпус 2 / 207", "e851985c-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 209", "0db53c6c-0ac9-11ec-ad2e-00155d879809" },
            { "Корпус 2 / 210", "e8519865-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 300", "1189ac14-3e98-11eb-b18b-00155d879809" },
            { "Корпус 2 / 302", "e851985a-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 303", "e851985f-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 304", "82b7eae2-3e8f-11eb-b18b-00155d879809" },
            { "Корпус 2 / 305", "82b7eaed-3e8f-11eb-b18b-00155d879809" },
            { "Корпус 2 / 306", "1189ac11-3e98-11eb-b18b-00155d879809" },
            { "Корпус 2 / 308", "0db53c6a-0ac9-11ec-ad2e-00155d879809" },
            { "Корпус 2 / 309", "e851985e-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 310", "0db53c68-0ac9-11ec-ad2e-00155d879809" },
            { "Корпус 2 / 400", "82b7eae8-3e8f-11eb-b18b-00155d879809" },
            { "Корпус 2 / 402", "e851985d-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 403", "e8519864-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 404", "82b7eaea-3e8f-11eb-b18b-00155d879809" },
            { "Корпус 2 / 405", "e8519863-4017-11eb-b18b-00155d879809" },
            { "Корпус 2 / 407", "82b7eae7-3e8f-11eb-b18b-00155d879809" },
            { "Корпус 2 / 408", "1189ac13-3e98-11eb-b18b-00155d879809" },
            { "Корпус 2 / 409", "1189ac12-3e98-11eb-b18b-00155d879809" },
            { "Корпус 2 / маст 2 эт", "fb88c0d1-3fcb-11ee-9626-00155d879809" },
            { "Корпус 2 / Трен Зал", "fb88c0d2-3fcb-11ee-9626-00155d879809" },
            { "Общежитие / ?1", "2237f93b-489e-11ee-9626-00155d879809" },
            { "Общежитие / ?2", "03b84590-883b-11ee-b4f8-c6fea011313f" },
            { "Общежитие / ТИР", "92a98506-2b2e-11ec-8ea5-00155d879809" },
            { "Тир / Тир", "231c75ec-6f1e-11ee-9bfc-c6fea011313f" }
        };

        public static string GetIDValue(string key, RequestType type)
        {
            string value = type switch
            {
                RequestType.GroupId => GroupIDs[key],
                RequestType.TeacherId => TeacherIDs[key],
                RequestType.ClassroomId => ClassroomIDs[key],
                _ => string.Empty,
            };
            return value;
        }

        public static Dictionary<string, string> GetIDDict(RequestType type)
        {
            return type switch
            {
                RequestType.GroupId => GroupIDs,
                RequestType.TeacherId => TeacherIDs,
                RequestType.ClassroomId => ClassroomIDs,
                RequestType.None => [],
                _ => [],
            };
        }
    }
}
