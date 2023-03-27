create database currencydev;

CREATE TABLE IF NOT EXISTS "CurrencyInformation" (
    "Id" uuid PRIMARY KEY,
    "CurrencyCBRId" text NOT NULL,
    "CurrencyName" text NOT NULL,
    "CurrencyEngName" text NULL,
    "Nominal" int NOT NULL,
    "ParentCode" text NULL
);


CREATE TABLE IF NOT EXISTS "CurrencyRate" (
    "Id" uuid PRIMARY KEY,
    "Rate" numeric(18,6) NOT NULL,
    "CurrencyId" uuid NOT NULL REFERENCES "CurrencyInformation"("Id"),
    "RateDate" date NOT NULL
);
