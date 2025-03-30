import { useEffect, useState } from "react"
import "./css/PagePicker.css"

export default function PagePicker({ value, pageCount, onValueChange, ...props }) {
    const [displayedValue, setDisplayedValue] = useState(value + 1)

    function changePage(newValue) {
        if (newValue >= pageCount) newValue = pageCount - 1
        if (newValue < 0) newValue = 0
        setDisplayedValue(newValue + 1)
        onValueChange(newValue)
    }

    useEffect(() => {
        setDisplayedValue(value + 1)
    }, [value])

    return (
        <div className="pagepicker-container" {...props}>
            <button className="triangle-buttons" onClick={() => changePage(Number(value) - 1)}>
                <div className="triangle-buttons__triangle triangle-buttons__triangle--l"></div>
            </button>
            <input className="page-num" type="number" value={displayedValue}
                onChange={(event) => setDisplayedValue(event.target.value)}
                onBlur={(event) => changePage(Number(event.target.value) - 1)} />
            /{pageCount}
            <button className="triangle-buttons" onClick={() => changePage(Number(value) + 1)}>
                <div className="triangle-buttons__triangle triangle-buttons__triangle--r"></div>
            </button>
        </div>
    )
}
