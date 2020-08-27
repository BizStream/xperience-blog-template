import './text-editor.scss'
import MediumEditor from 'medium-editor'

const init = ({ editor, propertyName }) => {
  // const config = {
  //   toolbar: {
  //     buttons: [
  //       'bold',
  //       'italic',
  //       'underline',
  //       'orderedlist',
  //       'unorderedlist',
  //       'h1',
  //       'h2',
  //       'h3'
  //     ]
  //   },
  //   imageDragging: false,
  //   extensions: {
  //     imageDragging: {}
  //   }
  // }

  const config = {}

  // if (editor.dataset.enableFormatting === "False") {
  // config.toolbar = false;
  // config.keyboardCommands = false;
  // config.disableReturn = true;
  // }

  const mediumEditor = new MediumEditor(editor, config)
  mediumEditor.subscribe('editableInput', () => {
    const event = new CustomEvent('updateProperty', {
      detail: {
        name: propertyName,
        value: mediumEditor.getContent(),
        refreshMarkup: false
      }
    })

    editor.dispatchEvent(event)
  })
}

const destroy = ({ editor }) => {
  const mediumEditor = MediumEditor.getEditorFromElement(editor)
  if (mediumEditor) mediumEditor.destroy()
}

const dragStart = ({ editor }) => {
  const mediumEditor = MediumEditor.getEditorFromElement(editor)
  const focusedElement = mediumEditor && mediumEditor.getFocusedElement()

  const focusedMediumEditor =
    focusedElement && MediumEditor.getEditorFromElement(focusedElement)

  const toolbar =
    focusedMediumEditor && focusedMediumEditor.getExtensionByName('toolbar')

  if (focusedElement && toolbar) {
    toolbar.hideToolbar()
    focusedElement.removeAttribute('data-medium-focused')
  }
}

window.kentico.pageBuilder.registerInlineEditor('text-editor', {
  destroy,
  init,
  dragStart
})
