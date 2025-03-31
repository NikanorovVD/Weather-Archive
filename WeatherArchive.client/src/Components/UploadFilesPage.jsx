import { useState } from "react";

export default function UploadFilesPage() {
    const [files, setFiles] = useState([]);
    const [uploadInProgress, setUploadInProgress] = useState(false)
    const [uploadEnds, setUploadEnds] = useState(false)
    const [success, setSuccess] = useState(false)
    const [errors, setErrors] = useState([])

    const handleSubmit = async (event) => {
        event.preventDefault()
        if (!files.length) return alert('Пожалуйста, выберите файлы.');
        setUploadEnds(false)
        setUploadInProgress(true)

        const formData = new FormData();
        for (let file of files) {
            formData.append('files', file);
        }

        const response = await fetch('/weather/upload', {
            method: 'POST',
            body: formData,
        });

        if (response.ok) {
            setSuccess(true)
            setErrors([])
        }
        else {
            setSuccess(false)
            if (response.status == 400) {
                const json = await response.json()
                let errorStrings = Object.entries(json.errors).map(([key, value]) => `${key}: ${value}`)
                setErrors(errorStrings)
            }
            if (response.status == 500) {
                setErrors(["ошибка сервера"])
            }
        }
        setUploadEnds(true)
        setUploadInProgress(false)
    };

    return (
        <>
            <form style={{ marginLeft: 50, marginTop: 20 }} onSubmit={handleSubmit}>
                <label htmlFor="files">Выберите файлы:</label>
                <input id="files" type="file" accept=".xls, .xlsx" multiple onChange={(event) => setFiles(event.target.files)} />
                <button type="submit" disabled={uploadInProgress}>Загрузить</button>
            </form>

            {uploadInProgress &&
                <h2>Идёт загрузка...</h2>
            }

            {uploadEnds && success &&
                <h2>Загрузка завершена успешно</h2>
            }

            {uploadEnds && !success &&
                <>
                    <h2 className="error">Ошибка: </h2>
                    {errors.map(e => <h3 className="error" key={e}>{e}</h3>)}
                </>
            }
        </>
    );

}