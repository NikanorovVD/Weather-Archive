import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HomePage from './Components/HomePage';
import UploadFilesPage from './Components/UploadFilesPage';
import WatchArchivePage from './Components/WatchArchivePage';
import Navbar from './Components/Navbar';

function App() {
    return (
        <BrowserRouter>
            <Navbar />
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/archive" element={<WatchArchivePage />} />
                <Route path="/upload" element={<UploadFilesPage />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;