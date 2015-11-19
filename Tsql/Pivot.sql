-- table: VendorID | IncomeDay | IncomeAccount

select * from DailyIncome
Pivot (
    Max(IncomeAccount) for IncomeDay in ([Mon],[Tue],[Wed],[Thu],[Fri]) as MaxIncomePerDay
)
where VendorID in ('Spike')
