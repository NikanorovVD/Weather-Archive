import "./css/WeatherDataTable.css"

export default function WeatherDataTable({ data }) {
    function extractDateOnly(dateTime) {
        return new Date(dateTime).toISOString().split('T')[0].replaceAll('-', '.')
    }

    function extractTimeOnly(dateTime) {
        const date = new Date(dateTime)
        const hourses = date.getUTCHours().toString().padStart(2, '0')
        const minutes = date.getUTCMinutes().toString().padStart(2, '0')
        return `${hourses}:${minutes}`
    }

    return (
        <>
            {data != undefined &&
                <table>
                    <thead>
                        <tr>
                            <th className="date">Дата</th>
                            <th>Время</th>
                            <th>T</th>
                            <th>Влажность</th>
                            <th>Td</th>
                            <th>Давление</th>
                            <th>Направление ветра</th>
                            <th>Скорость ветра</th>
                            <th>Облачность</th>
                            <th>h</th>
                            <th>VV</th>
                            <th>Погодные явления</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map(x =>
                            <tr key={x.dateTime}>
                                <td className="date">{extractDateOnly(x.dateTime)}</td>
                                <td>{extractTimeOnly(x.dateTime)}</td>
                                <td>{x.temperature}</td>
                                <td>{x.relativeHumidity}</td>
                                <td>{x.td}</td>
                                <td>{x.atmosphericPressure}</td>
                                <td>{x.windDirection}</td>
                                <td>{x.windSpeed}</td>
                                <td>{x.cloudCover}</td>
                                <td>{x.h}</td>
                                <td>{x.vv}</td>
                                <td>{x.weatherPhenomena}</td>
                            </tr>
                        )}
                    </tbody>
                </table>}

            {data != undefined && data.length == 0 &&
                <h2>Нет данных</h2>}
        </>
    )
}