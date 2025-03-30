import "./css/Navbar.css"

export default function Navbar() {
    return (
        <nav className="navbar">
            <ul>
                <li><a href="/">Главная</a></li>
                <li><a href="/archive">Просмотр архива</a></li>
                <li><a href="/upload">Загрузить файлы</a></li>
                <li><a href="https://localhost:7056/swagger/index.html">API</a></li>
            </ul>
        </nav>
    )
}