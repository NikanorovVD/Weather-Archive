import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HomePage from './Components/HomePage';
import UploadFilesPage from './Components/UploadFilesPage';
import WatchArchivePage from './Components/WatchArchivePage';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/archive" element={<WatchArchivePage />} />
                <Route path="/upload" element={<UploadFilesPage />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;