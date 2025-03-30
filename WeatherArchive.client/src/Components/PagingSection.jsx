import { useState, useEffect } from "react"
import PagePicker from "./PagePicker"
import "./css/PagingSection.css"

export default function PagingSection({ pageSize, pageNum, totalPages, onPageSizeChanged, onPageNumChanged, ...props }) {
    const [pageSizeDisplayed, setPageSizeDisplayed] = useState(pageSize)

    useEffect(() => {
        setPageSizeDisplayed(pageSize)
    }, [pageSize])

    return (
        <section {...props}>
            <div className="paging">
                <label>Размер страницы: </label>
                <input className="page-size" type="number" value={pageSizeDisplayed}
                    onChange={(event) => setPageSizeDisplayed(event.target.value)}
                    onBlur={(event) => onPageSizeChanged(event.target.value)} />
            </div>

            <div className="paging paiging-container">
                <label >Страница:</label>
                <PagePicker value={pageNum} pageCount={totalPages} onValueChange={onPageNumChanged}></PagePicker>
            </div>
        </section>
    )

}