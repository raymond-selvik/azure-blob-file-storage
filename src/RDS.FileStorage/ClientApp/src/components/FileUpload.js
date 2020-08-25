import React, {Component} from 'react';
import { BsUpload } from "react-icons/bs";
import Modal from "react-bootstrap/Modal";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form'


export class FileUpload extends Component {
    static displayName = FileUpload.name;

    constructor(props) {
        super(props);

        this.state = {
            file: '',
            fileName: 'Choose file',
            uploadMessage: '',
            uploadedFileDesciption: null,
            showUploader: false
        }
    }

    handleClose = () => this.setState({showUploader: false});
    handleShow = () => this.setState({showUploader: true});

    onChange = e => {
        console.log(e.target.files);
        this.setState({
            file: e.target.files[0],
            fileName: e.target.files[0].name
        });
    }

    onSubmit = async e => {
        //e.PreventDefault();
        const formData = new FormData();
        formData.append('file', this.state.file);
        formData.append('path', this.props.dir);

        console.log(this.props.dir);

        await fetch('file/upload',
        {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + sessionStorage.tokenKey
            },
            body: formData
        })
        .then((response) => {
            if(!response.ok) throw new Error(response.status);
            else {
                console.log("jei");
                return response.json();
            }
        })
        .then((data) => {
            this.setState({
                uploadedFileDesciption: data
            })
        })
        .catch((error) => {
            console.error(error);
        });

        this.handleClose();
    }

    render() {
        return(
            <>
            <Button variant='primary' onClick={this.handleShow}>
                <BsUpload/>
            </Button>
            <Modal
                show = {this.state.showUploader}
                onHide = {this.handleClose}
                backdrop = "static"
                keyboard = {false}
            >
                <Modal.Header closeButton>
                    <Modal.Title>Upload File</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={this.onSubmit}>
                        <Form.File 
                            id="custom-file"
                            label={this.state.fileName}
                            custom
                            onChange={this.onChange}
                        />
                        <hr/>
                        <Button variant="secondary" type="submit">Upload</Button>
                    </Form>
                </Modal.Body>
            </Modal>
            </>
        )
    }
}
