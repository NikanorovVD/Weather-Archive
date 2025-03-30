import { useCallback, useEffect, useState } from "react"
import "./css/WatchArchivePage.css"
import WeatherDataTable from "./WeaterDataTable"
import PagingSection from "./PagingSection"
import FilterSection from "./FilterSection"

export default function WatchArchivePage() {
    const [data, setData] = useState()
    const [year, setYear] = useState()
    const [month, setMonth] = useState(1)
    const [pageSize, setPageSize] = useState(30)
    const [pageNum, setPageNum] = useState(0)
    const [availableYears, setAvailableYears] = useState()
    const [totalPages, setTotalPages] = useState(0)

    const fetchYears = useCallback(async () => {
        const response = await fetch('WeatherArchive/GetAvailableYears');
        const years = await response.json();
        setAvailableYears(years)
        setYear(years[0])
    }, [])

    const fetchData = useCallback(async (month, year, pageSize, pageNum) => {
        if (year == undefined) return

        const url = `WeatherArchive/GetWeatherRecords?year=${year}&month=${month}&pageSize=${pageSize}&pageNumber=${pageNum}`
        const response = await fetch(url);

        if (response.ok) {
            const jsonResponse = await response.json();
            setData(jsonResponse.data);
            setPageNum(jsonResponse.pageNumber)
            setPageSize(jsonResponse.pageSize)
            setTotalPages(jsonResponse.totalPages)
        }
    }, [])

    useEffect(() => {
        fetchYears()
    }, [])

    useEffect(() => {
        fetchData(month, year, pageSize, pageNum)
    }, [year, month, pageNum, pageSize])

    function changePageSize(newValue) {
        if (newValue <= 0) newValue = 1
        if (newValue >= 100) newValue = 100
        setPageSize(newValue)
        setPageNum(0)
    }

    function changeMonth(newValue) {
        setMonth(newValue)
        setPageNum(0)
    }

    function changeYear(newValue) {
        setYear(newValue)
        setPageNum(0)
    }

    return (
        <>
            <div className="container">
                <FilterSection
                    month={month}
                    year={year}
                    availableYears={availableYears}
                    onMonthChanged={changeMonth}
                    onYearChanged={changeYear}>
                </FilterSection>

                <PagingSection
                    pageSize={pageSize}
                    pageNum={pageNum}
                    totalPages={totalPages}
                    onPageNumChanged={setPageNum}
                    onPageSizeChanged={changePageSize}>
                </PagingSection>
            </div>
            <WeatherDataTable data={data}></WeatherDataTable>
            {availableYears != undefined && availableYears.length == 0 &&
                <h2 style={{ marginLeft: 80 }}>Нет данных</h2>
            }
        </>
    )
}