import MonthSelect from "./MonthSelect"
import "./css/FilterSection.css"

export default function FilterSection({ month, year, availableYears, onMonthChanged, onYearChanged, ...props }) {
    return (
        <section {...props}>
            <div className="filter">
                <label style={{ marginRight: 42 }}>Год:</label>
                <select value={year} onChange={(event) => onYearChanged(event.target.value)} >
                    {availableYears != undefined && availableYears.map(y =>
                        <option key={y}>{y}</option>
                    )}
                </select>
            </div>
            <div className="filter">
                <label>Месяц: </label>
                <MonthSelect value={month} onValueChanged={onMonthChanged}></MonthSelect>
            </div>
        </section>
    )
}