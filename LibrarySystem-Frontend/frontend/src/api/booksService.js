import { API_URL } from '../config/apiConfig';
import axios from "axios";

class booksService {
    async getAllBooks() {
        return (await axios.get(`${API_URL}/books`)).data;
    }

    async getBookById(bookId) {
        return (await axios.get(`${API_URL}/books/${bookId}`)).data;
    }

    async createBook(book) {
        return (await axios.post(`${API_URL}/books`, book)).data;
    }

    async delete(bookId) {
        return (await axios.delete(`${API_URL}/books/${bookId}`)).data;
    }

    async search(query) {
        return (await axios.get(`${API_URL}/books/search?query=${query}`)).data;
    }
}

const booksServiceInstance = new booksService();
export default booksServiceInstance;
