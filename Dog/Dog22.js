const baseUrl = "https://dogrest20240608155729.azurewebsites.net/api/Dog";

Vue.createApp({
    data() {
        return {
            dogs: [],
            newDog: {
                id: '',
                name: '',
                age: ''
            },
            addMessage: '',
            enkeldog: null,
            searchId: '', // Tilføjet searchId her
            searchResult: null // Tilføjet searchResult her
        }
    },
    created() {
        this.GetDogs();
    },
    methods: {
        async helperGetAndShow(baseUrl) {
            try {
                const response = await axios.get(baseUrl);
                this.dogs = response.data;
                console.log(this.dogs);
            } catch (ex) {
                alert(ex.message);
            }
        },
        GetDogs() {
            this.helperGetAndShow(baseUrl);
        },
        async getById(id) {
            const url = `${baseUrl}/${id}`;
            try {
                const response = await axios.get(url);
                this.searchResult = response.data;
            } catch (ex) {
                alert(ex.message);
            }
        },

        async AddDog() {
            try {
                const response = await axios.post(baseUrl, this.newDog);
                this.addMessage = `response ${response.status} ${response.statusText}`;
                this.GetDogs();
            } catch (ex) {
                alert(ex.message);
            }
        },

        async SortByName(){
            try {
                const response = await axios.get(`${baseUrl}/SortByName`);
                this.dogs = response.data;
            } catch (ex) {
                alert(ex.message);
            }
        },

        async SortByAge(){
            try {
                const response = await axios.get(`${baseUrl}/SortByAge`);
                this.dogs = response.data;
            } catch (ex) {
                alert(ex.message);
            }
        },

        async FilterByName(){
            try {
                const response = await axios.get(`${baseUrl}/FilterByName/${this.searchId}`);
                this.dogs = response.data;
            } catch (ex) {
                alert(ex.message);
            }
        },

        async DeleteDog(id){
            try {
                const response = await axios.delete(`${baseUrl}/${id}`);
                this.GetDogs();
            } catch (ex) {
                alert(ex.message);
            }

        }    
        }
}).mount("#app");
