import { useState } from "react";

export default function UploadFilesPage() {
    const [files, setFiles] = useState([]);
    const [uploadInProgress, setUploadInProgress] = useState(false)
    const [uploadEnds, setUploadEnds] = useState(false)

    const handleSubmit = async (event) => {
        event.preventDefault()
        if (!files.length) return alert('Пожалуйста, выберите файлы.');
        setUploadEnds(false)
        setUploadInProgress(true)
        const formData = new FormData();
        for (let file of files) {
            formData.append('files', file);
        }

        try {
            const response = await fetch('/weather/upload', {
                method: 'POST',
                body: formData,
            });

            if (!response.ok) throw new Error(`Ошибка: ${response.status}`);
            console.log('Файлы успешно загружены');
        } catch (error) {
            console.error(error.message);
        }
        setUploadEnds(true)
        setUploadInProgress(false)
    };

    return (
        <>
            <form style={{ marginLeft: 50, marginTop: 20 }} onSubmit={handleSubmit}>
                <label htmlFor="files">Выберите файлы:</label>
                <input id="files" type="file" multiple onChange={(event) => setFiles(event.target.files)} />
                <button type="submit" disabled={uploadInProgress}>Загрузить</button>
            </form>

            {uploadInProgress &&
                <h2>Идёт загрузка...</h2>
            }

            {uploadEnds &&
                <h2>Загрузка завершена</h2>
            }
        </>
    );

}