import "./css/HomePage.css"

export default function HomePage() {
    return (
        <section>
            <div className="header">
                <h1>Архив погодных условий - Москва</h1>
            </div>
            <nav>
                <ul>
                    <li><a href="/archive">Просмотр архива</a></li>
                    <li><a href="/upload">Загрузить файлы</a></li>
                </ul>
            </nav>
        </section>
    )
}
